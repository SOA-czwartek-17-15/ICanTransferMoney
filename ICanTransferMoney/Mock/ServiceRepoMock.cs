using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
namespace ICanTransferMoney.Mock
{
    class ServiceRepoMock : IServiceRepository
    {
        public string GetServiceAddress(string serviceName)
        {
            throw new NotImplementedException();
        }

        public void IsAlive(string serviceName)
        {
            throw new NotImplementedException();
        }

        public void RegisterService(string serviceName, string serviceAddress)
        {
            throw new NotImplementedException();
        }

        public void UnregisterService(string serviceName)
        {
            throw new NotImplementedException();
        }
    }
}
