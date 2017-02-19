using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLib
{
    public class Bank<T> where T : Account
    {
        T[] accounts;

        public string Name { get; private set; }

        public Bank(string name)
        {
            Name = name;
        }

        

        public void Open(AccountType accountType, decimal sum, AccountStateHandler addSumHandler,
            AccountStateHandler withdarwSumHandler,
            AccountStateHandler calculationHandler, AccountStateHandler closeAccountHandler,
            AccountStateHandler openAccountHandler)
        {
            T newAccount = null;

            switch (accountType)
            {
                    case AccountType.Ordinary:
                        newAccount = new DemandAccount(sum, 1) as T;
                        break;
                    case AccountType.Deposit:
                        newAccount = new DepositAccount(sum, 40) as T;
                        break;
            }

            if(newAccount == null)
                throw new Exception("Error creating");
            if (accounts == null)
                accounts = new T[] {newAccount};
            else
            {
                T[] tempAccounts = new T[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; i++)
                    tempAccounts[i] = accounts[i];
                tempAccounts[tempAccounts.Length - 1] = newAccount;
                accounts = tempAccounts;
            }

            newAccount.Added += addSumHandler;
            newAccount.Whithdrawed += withdarwSumHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Calculated += calculationHandler;

            newAccount.Open();
        }

        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("No bils finded");
            account.Withdraw(sum);
        }

        public void Withdraw(decimal sum, int id)
        {
            T account = FindAccount(id);
            if(account == null)
                throw new Exception("Cant finde the bill");
            account.Withdraw(sum);
        }

        public void Close(int id)
        {
            int index;
            T account = FindAccount(id, out index);
            if (account == null)
                throw new Exception("Cant finde the bill");

            account.Close();

            if (accounts.Length <= 1)
                account = null;
            else
            {
                T[] tempAccounts = new T[accounts.Length - 1];
                for (int i = 0, j = 0; i < accounts.Length; i++)
                {
                    if (i != index)
                        tempAccounts[++j] = accounts[i];
                }
                accounts = tempAccounts;
            }
        }

        public void CalculatePercentage()
        {
            if (accounts == null)
                return;
            for (int i = 0; i < accounts.Length; i++)
            {
                T account = accounts[i];
                account.IncrementDays();
                account.Calculate();
            }
        }

        public T FindAccount(int id)
        {
            return accounts.FirstOrDefault(t => t.Id == id);
        }

        public T FindAccount(int id, out int index)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id != id) continue;
                index = i;
                return accounts[i];
            }
            index = -1;
            return null;
        }
    }
    public enum AccountType
    {
        Ordinary,
        Deposit
    }
}
