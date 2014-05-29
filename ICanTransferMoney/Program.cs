using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Contracts;
using System.Timers;
using log4net;
using log4net.Config;
using System.Xml;
namespace ICanTransferMoney
{
    class Program
    {
        static string serviceRepoAddress = "";
        static string localAddress = "";
        static Timer keepAliveTimer;
        static Timer connectTimer;
        static ILog log = LogManager.GetLogger("Program");
        static ServiceHost serviceHost;
        static IServiceRepository serviceRepo;

        static void Main(string[] args)
        {
            BasicConfigurator.Configure();
            // Wczytanie konfiguracji
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("../../config.xml");
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Configuration file not found!");
                return;
            }
            
            serviceRepoAddress = doc.GetElementsByTagName("serviceRepoAddress")[0].InnerText;
            localAddress = doc.GetElementsByTagName("localAddress")[0].InnerText;

            if (serviceRepoAddress == null || localAddress == null)
            {
                Console.WriteLine("Service Repository address or local address not found in config file");
                return;
            }

            // Próba połączenia
            connectTimer = new Timer(5000);  // Próbuj łączyć się co 5s
            connectTimer.Elapsed += new ElapsedEventHandler(BuildConnectionsFramework);

            BuildConnectionsFramework();

            Console.WriteLine("ICanTransferMoney is running.");
            Console.WriteLine("Hit any key to prepare shutdown.");
            Console.ReadLine();
            Console.WriteLine("Click once again to shutdown service.");
            Console.ReadLine();
            UnregisterService();
            Console.WriteLine("Service closed. Press any key to continue.");
            Console.ReadLine();
        }

        private static void BuildConnectionsFramework(object sender = null, ElapsedEventArgs e = null)
        {
            // set up data sources
            try
            {
                Initialize();
                connectTimer.Stop();
            }
            catch(EndpointNotFoundException)
            {
                Console.WriteLine("One of services not found. Will try again every 5s.");
                if(!connectTimer.Enabled)
                    connectTimer.Start();
            }
        }

        static void Initialize()
        {
            // set up service
            Console.WriteLine("Initializing services...");
            IServiceFactory serviceFactory = new ServiceConnector(serviceRepoAddress);

            serviceRepo = serviceFactory.GetServiceRepository();
            MoneyTransferer transferer = new MoneyTransferer(serviceFactory);

            OpenService(transferer);
            RegisterService(serviceRepo);
        }


        private static void OpenService(Contracts.ICanTransferMoney service)
        {
            log.Info("Opening service endpoint");
            serviceHost = new ServiceHost(service, new Uri[] { new Uri(localAddress) });
            NetTcpBinding serverBinding = new NetTcpBinding(SecurityMode.None);
            serviceHost.AddServiceEndpoint(typeof(Contracts.ICanTransferMoney), serverBinding, localAddress);
            serviceHost.Open();
            log.Info("Service endpoint opened");
        }

        private static void RegisterService(IServiceRepository serviceRepo)
        {
            log.Info("Registering service in ServiceRepository");
            serviceRepo.RegisterService("ICanTransferMoney", localAddress);
            log.Info("Service registered");
            
            AliveKeeper keeper = new AliveKeeper(serviceRepo);
            keepAliveTimer = new Timer(3000);
            keepAliveTimer.Elapsed += new ElapsedEventHandler(keeper.KeepAlive);
            keepAliveTimer.Enabled = true;
        }

        private static void UnregisterService()
        {
            log.Info("Unregistering service");
            try
            {
                serviceRepo.Unregister("ICanTransferMoney");
                serviceHost.Close();
            }
            catch (TimeoutException)
            {

            }
            keepAliveTimer.Enabled = false;
            log.Info("Service unregistered");
        }
    }
}
