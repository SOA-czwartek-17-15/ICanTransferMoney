using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace ICanTransferMoney
{
    interface IServiceFactory
    {
        public IServiceRepository GetServiceRepository();
        public IAccountRepository GetAccountRepository();
        public IAuditorService GetAuditorService();
    }
}
