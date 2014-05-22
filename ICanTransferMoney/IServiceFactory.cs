using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace ICanTransferMoney
{
    public interface IServiceFactory
    {
        IServiceRepository GetServiceRepository();
        IAccountRepository GetAccountRepository();
        IAuditorService GetAuditorService();
    }
}
