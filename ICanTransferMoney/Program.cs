using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ICanTransferMoney
{
    class Program
    {
        const string SERVICE_ADDRESS = "net.tcp://0.0.0.0:41234/ICanTransferMoney";

        static void Main(string[] args)
        {
            ICanTransferMoney bank = new MoneyTransferer();
            var sh = new ServiceHost(bank, new Uri[] { new Uri(SERVICE_ADDRESS) });
            NetTcpBinding serverBinding = new NetTcpBinding();
            sh.AddServiceEndpoint(typeof(ICanTransferMoney), serverBinding, SERVICE_ADDRESS);

            sh.Open();

            // Łączymy się na ServiceRepository
            NetTcpBinding srBinding = new NetTcpBinding();
            ChannelFactory<IServiceRepository> cf = new ChannelFactory<IServiceRepository>(srBinding, new EndpointAddress("net.tcp://localhost:41234/IServiceRepository"));
            IServiceRepository isr = cf.CreateChannel();
            isr.RegisterService("ICanTransferMoney", SERVICE_ADDRESS);

            Console.ReadLine();
        }
    }
}
