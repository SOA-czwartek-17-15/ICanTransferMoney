using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ICanTransferMoney
{
    class Program
    {
        private static string serviceRepoAddress;
        private static string serviceRepoPort;
        private static string localPort;

        static void Main(string[] args)
        {
            if (!Configure())
                return;

            Console.WriteLine("Connecting to Service Repository");
            ServiceRepository serviceRepo = new ServiceRepository(buildServiceRepoLocation());
            Console.WriteLine("Service Repository connected. Fetching service locations.")
            ServiceLocations serviceLocs = serviceRepo.GetServiceLocations();
            Console.WriteLine("Service locations fetched.");

            Console.WriteLine("Connecting to Account Repository");
            AccountRepository accountRepo = new AccountRepository(serviceLocs.accountRepoAddress);
            AuditorService auditorServ = new AuditorService(serviceLocs.auditorAddress);

            if (!serviceRepo.RegisterMe())
                return;

            MoneyTransferService mts = new MoneyTransferService(accountRepo,auditorServ);

            mts.Dispose();

            serviceRepo.UnregisterMe();

            Console.WriteLine("Program się zakończy");
            Console.ReadLine();
        }

        private static string buildServiceRepoLocation()
        {
            StringBuilder sb = new StringBuilder("tcp://");
            sb.Append(serviceRepoAddress);
            sb.Append(":");
            sb.Append(int.Parse(serviceRepoPort));
            return sb.ToString();
        }



        private static bool Configure()
        {
            try
            {

                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count < 3)
                {
                    Console.WriteLine("Niepelny zestaw danych konfiguracyjnych");
                    Console.ReadLine();
                    return false;
                }

                serviceRepoAddress = appSettings["ServiceRepoAddress"];
                serviceRepoPort = appSettings["ServiceRepoPort"];
                localPort = appSettings["LocalPort"];

                if (serviceRepoAddress == null || serviceRepoPort == null || localPort == null)
                {
                    Console.WriteLine("Brak odpowiednich pól w konfiguracji");
                    return false;
                }

                return true;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Błąd wczytywania konfiguracji");
                return false;
            }
        }

    }
}
