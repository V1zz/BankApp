using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLib
{
    class DepositAccount : Account
    {
        public DepositAccount(decimal sum, int percentage) : base(sum, percentage) { }

        protected internal override void Open()
        {
            base.Open();
        }

        public override void Put(decimal sum)
        {
            if(_days % 30 == 0)
                base.Put(sum);
            else 
                base.OnWithdrawed(new AccountEventArgs("Withdrowing can be made after 30th day", 0));
        }

        protected internal override void Calculate()
        {
            if(_days % 30 == 0 )
                base.Calculate();
        }
    }
}
