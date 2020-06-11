using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            string userChoice = "";
            
            Password testpassword;

            while (true)
            {
                Console.WriteLine("Welcome to the Password Checker! Please chose an option:");
                Console.WriteLine("1. Check Password \n2. Generate Password \n3. Quit");
                userChoice = Console.ReadLine();

                //Using a switch statement allows for easy identification of different inputs, and what to do in their cases
                switch (userChoice) 
                {
                    case "1":
                        StringBuilder userPassword = new StringBuilder();

                        Console.WriteLine("Enter your password below:");
                        userPassword.Append(Console.ReadLine());

                        testpassword = new Password(userPassword);
                        break;

                    case "2":
                        Console.WriteLine("Generating Password...");
                        testpassword = new Password();
                        break;

                    case "3":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Press Enter to continue.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
