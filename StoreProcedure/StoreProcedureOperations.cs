using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoreProcedure
{
    public class StoreProcedureOperations
    {
        string connectionString = @"data source = PRAJWAL; database = OrderManagementSystem ; integrated security = true";
        SqlConnection connection;
        public StoreProcedureOperations() {
            connection = new SqlConnection(connectionString);
        }

        public bool AddDataUsingTransactionStoreProcedure(string name, string lastname, string Email,string OrderDate,string OrderTotal)
        {
            try
            {    
                connection.Open();
                // command part 
                string query = "AddOrderNewCustomerTransactionalNew";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@FirstName", name);
                sqlCommand.Parameters.AddWithValue("@LastName", lastname);
                sqlCommand.Parameters.AddWithValue("@EmailId", Email);
                sqlCommand.Parameters.AddWithValue("@OrderDate", OrderDate);
                sqlCommand.Parameters.AddWithValue("@OrderTotal", OrderTotal);
                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine($"{result} rows affected ");
                    Console.WriteLine("Data added .....");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Something Went wrong.....");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool AddDataUsingNONTransactionStoreProcedure(string name, string lastname, string Email, string OrderDate, string OrderTotal)
        {
            try
            {
                // here we are using normal stored procedure but we implement try catch block here insted of stored procedure 
                // here any one of the tables data is wrong then there is no rollback and no commit so you haave to make this 
                // transactional so for that add try catch block as follows 
                connection.Open();
                string Query = "AddOrderNewCustomer";

                SqlTransaction sqlTransaction = connection.BeginTransaction(); /// add this to make it transactional ->1
                SqlCommand sqlCommand = new SqlCommand(Query,connection,sqlTransaction); /// paas that object here    ->2
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@FirstName", name);
                sqlCommand.Parameters.AddWithValue("@LastName", lastname);
                sqlCommand.Parameters.AddWithValue("@EmailId", Email);
                sqlCommand.Parameters.AddWithValue("@OrderDate", OrderDate);
                sqlCommand.Parameters.AddWithValue("@OrderTotal", OrderTotal);
                try                                                                /// add this try catch block to make it transactional  ->3
                {
                   int result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();                                          /// -> 4
                    Console.WriteLine($"{result} rows affected ");
                    Console.WriteLine("Data added .....");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Rolling Back the Changes");
                    sqlTransaction.Rollback();                                      /// -> 5
                    Console.WriteLine(ex);
                    
                }

                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                Console.WriteLine("Something Went wrong....");
                return false;
            }
            finally
            {
                connection.Close() ;
            }
        }
        public bool Display()
        {
            try
            {
                List<Order> list = new List<Order>();
                connection.Open ();
                string Query = "GetOrderDetails";
                SqlCommand sqlCommand = new SqlCommand(Query,connection);
                
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read()) {
                    Order order = new Order()
                    {
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        OrderDate= (DateTime)reader["OrderDate"],
                        OrderTota = (decimal)reader["OrderTotal"]
                    };
                    list.Add(order);
                }

                foreach (Order order in list)
                {
                    Console.WriteLine($"FirstName:- {order.FirstName}\t LastName:- {order.LastName} \t OrderDate:- {order.OrderDate} \t OrderTotal:- {order.OrderTota}");

                }
                connection.Close(); 
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Something went wrong");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}