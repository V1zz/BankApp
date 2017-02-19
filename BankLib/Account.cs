using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankLib
{
    public abstract class Account : IAccount
    {
        int? a = null;
        protected internal event AccountStateHandler Whithdrawed;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        protected internal event AccountStateHandler Closed;
        protected internal event AccountStateHandler Calculated;

        protected int _id;
        static int counter = 0;

        protected decimal _sum;
        protected int _percentage;

        protected int _days = 0;

        public Account(decimal sum, int percentage)
        {
            _sum = sum;
            _percentage = percentage;
            _id = ++counter;
        }

        public decimal CurrentSum => _sum;
        public int Percentage => _percentage;
        public int Id => _id;

        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (handler != null && e != null)
                handler(this, e);
        }

        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }

        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Whithdrawed);
        }

        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }

        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }

        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }
        
        public virtual void Put(decimal sum)
        {
            _sum += sum;
            OnAdded(new AccountEventArgs("Added: " + sum, sum));
        }

        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (sum <= _sum)
            {
                _sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($"Sum {sum} withdarwed from {_id}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs("No money on: " + _id, sum));
            }
            return result;
        }

        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs("Opened new deposit with id: " + this._id, this._sum));
        }

        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Bill: {_id} Current _sum is: {CurrentSum:F}", CurrentSum));
        }

        protected internal void IncrementDays()
        {
            _days++;
        }

        protected internal virtual void Calculate()
        {
            decimal increment = _sum*_percentage/100;
            _sum += increment;
            OnCalculated(new AccountEventArgs("Your uncrement is: " + increment, increment));
        }
        
    }
}
