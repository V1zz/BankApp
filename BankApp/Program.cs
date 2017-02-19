using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLib;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("UniCredit");
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(@"
1. Create bill
2. Withrow money
3. Put money
4. Close the bill
5. <nothing>
5. Exit
");
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter SUM for creating the bill: ");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine(@"Wich one type?
    1. Demand
    2. Deposit");

            int type = Convert.ToInt32(Console.ReadLine());

            var accountType = type == 2 ? AccountType.Deposit : AccountType.Ordinary;

            bank.Open(accountType,
                sum,
                AddSumHandler,
                WithdrawSumHandler,
                (o, e) => Console.WriteLine(e.Message),
                CloseAccountHandler,
                OpenAccount);
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("How mach money you want to withraw?");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("How mach money you want to put?");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("In what id you want to put?");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter ID to cloce the bill");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }

        private static void OpenAccount(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            if (e.Sum > 0)
                Console.WriteLine("You got a money on drugs");
        }

        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
