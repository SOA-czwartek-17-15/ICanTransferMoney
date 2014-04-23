using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICanTransferMoney
{
    interface ICanTransferMoney
    {
        public bool transferMoney(Account from, Account to);
    }
}
