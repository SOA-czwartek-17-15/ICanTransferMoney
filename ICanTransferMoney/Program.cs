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
            IServiceRepository serviceRepo = ConnectServiceRepository();
            MoneyTransferer transferer = new MoneyTransferer(serviceRepo);
            OpenService(transferer);
            RegisterService(serviceRepo);

            Console.ReadLine();
        }

        private static IServiceRepository ConnectServiceRepository()
        {
            NetTcpBinding srBinding = new NetTcpBinding();
            ChannelFactory<IServiceRepository> cf = new ChannelFactory<IServiceRepository>(srBinding, new EndpointAddress("net.tcp://localhost:41234/IServiceRepository"));
            return cf.CreateChannel();
        }

        private static void OpenService(Contracts.ICanTransferMoney transferer)
        {
            var sh = new ServiceHost(transferer, new Uri[] { new Uri(SERVICE_ADDRESS) });
            NetTcpBinding serverBinding = new NetTcpBinding();
            sh.AddServiceEndpoint(typeof(Contracts.ICanTransferMoney), serverBinding, SERVICE_ADDRESS);

            sh.Open();
        }

        private static void RegisterService(IServiceRepository isr)
        {
            isr.RegisterService("ICanTransferMoney", SERVICE_ADDRESS);
        }
    }
}
