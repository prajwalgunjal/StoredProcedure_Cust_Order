using System.Data.SqlClient;

namespace StoreProcedure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StoreProcedureOperations operations = new StoreProcedureOperations();
            Console.WriteLine("Press 1 : Add Data Using Transaction Store Procedure");
            Console.WriteLine("Press 2 : Add Data Using Non Transaction Store Procedure");
            Console.WriteLine("Press 3 : Display");
            Console.WriteLine("Enter Your Choice");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("Enter name:- ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Lastname:- ");
                        string lastname = Console.ReadLine();
                        Console.WriteLine("Enter Email ID:- ");
                        string Email = Console.ReadLine();
                        Console.WriteLine("Enter OrderDate:- ");
                        string OrderDate = Console.ReadLine();
                        Console.WriteLine("Enter OrderTotal:- ");
                        string OrderTotal = Console.ReadLine();
                        operations.AddDataUsingTransactionStoreProcedure(name,lastname,Email,OrderDate,OrderTotal);
                        break;
                    }
                    case 2:
                    {
                        Console.WriteLine("Enter name:- ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Lastname:- ");
                        string lastname = Console.ReadLine();
                        Console.WriteLine("Enter Email ID:- ");
                        string Email = Console.ReadLine();
                        Console.WriteLine("Enter OrderDate:- ");
                        string OrderDate = Console.ReadLine();
                        Console.WriteLine("Enter OrderTotal:- ");
                        string OrderTotal = Console.ReadLine();
                        operations.AddDataUsingNONTransactionStoreProcedure(name, lastname, Email, OrderDate, OrderTotal);
                        break;
                    }
                case 3:
                    {
                        operations.Display();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Please Enter Valid Choice........");
                        break;
                    }

            }

        }
    }
}