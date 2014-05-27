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


        public void Alive(string Name)
        {
            Console.WriteLine("Alive sent to service repository");
        }

        public string GetServiceLocation(string Name)
        {
            return Name;
        }

        public void RegisterService(string Name, string Address)
        {
            Console.WriteLine("Service " + Name + " registered on address "+Address);
        }

        public void Unregister(string Name)
        {
            Console.WriteLine("Service " + Name + " unregistered from service repo");
        }
    }
}
