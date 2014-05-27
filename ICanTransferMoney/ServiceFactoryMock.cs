using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace ICanTransferMoney
{
    class ServiceFactoryMock : IServiceFactory
    {
        private IServiceRepository serviceRepoMock;
        private IAccountRepository accountRepoMock;
        private IAuditorService auditorServMock;

        public ServiceFactoryMock()
        {
            serviceRepoMock = new Mock.ServiceRepoMock();
            accountRepoMock = new Mock.AccountRepoMock();
            auditorServMock = new Mock.AuditorServMock();
        }

        public Contracts.IServiceRepository GetServiceRepository()
        {
            return serviceRepoMock;
        }

        public Contracts.IAccountRepository GetAccountRepository()
        {
            return accountRepoMock;
        }

        public Contracts.IAuditorService GetAuditorService()
        {
            return auditorServMock;
        }
    }
}
