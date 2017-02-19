using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLib
{
    public interface IAccount
    {
        void Put(decimal sum);
        decimal Withdraw(decimal sum);
    }

}
