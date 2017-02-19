using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLib
{
    class DemandAccount : Account
    {
        public DemandAccount(decimal sum, int percentage) : base(sum, percentage) { }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs("Opened new bill. Id is: " + this._id, this._sum));
        }
    }
}
