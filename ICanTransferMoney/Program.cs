using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Contracts;

namespace ICanTransferMoney
{
    class Program
    {
        const string SERVICE_ADDRESS = "net.tcp://0.0.0.0:41234/ICanTransferMoney";

        static void Main(string[] args)
        {
            // set up data sources
            ServiceConnector serviceConnector = ServiceConnector.Instance;

            // set up service
            MoneyTransferer transferer = new MoneyTransferer(serviceConnector);
            OpenService(transferer);
            
            // register service
            IServiceRepository serviceRepo = serviceConnector.GetServiceRepository();
            RegisterService(serviceRepo);

            Console.ReadLine();
        }


        private static void OpenService(Contracts.ICanTransferMoney service)
        {
            var sh = new ServiceHost(service, new Uri[] { new Uri(SERVICE_ADDRESS) });
            NetTcpBinding serverBinding = new NetTcpBinding();
            sh.AddServiceEndpoint(typeof(Contracts.ICanTransferMoney), serverBinding, SERVICE_ADDRESS);
            sh.Open();
        }

        private static void RegisterService(IServiceRepository serviceRepo)
        {
            serviceRepo.RegisterService("ICanTransferMoney", SERVICE_ADDRESS);
        }
    }
}
