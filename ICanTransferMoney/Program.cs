﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Contracts;
using System.Timers;
using log4net;
using log4net.Config;
namespace ICanTransferMoney
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        const string SERVICE_ADDRESS = "net.tcp://192.168.0.98:50000/ICanTransferMoney";
        static Timer keepAliveTimer; 
        static void Main(string[] args)
        {
            BasicConfigurator.Configure();

            // set up data sources
            IServiceFactory serviceFactory = ServiceConnector.Instance;

            // set up service
            MoneyTransferer transferer = new MoneyTransferer(serviceFactory);
            OpenService(transferer);
            
            // register service
            IServiceRepository serviceRepo = serviceFactory.GetServiceRepository();
            RegisterService(serviceRepo);

            var accountRepo = serviceFactory.GetAccountRepository();
            var auditServ = serviceFactory.GetAuditorService();
            
            // mocking accounts
            /*Console.ReadLine();
            Console.WriteLine("Mocking accounts");
            var accRepo = (Mock.AccountRepoMock)serviceFactory.GetAccountRepository();
            Account someAccount1 = new Account();
            Guid acc1guid = new Guid();
            someAccount1.Id = acc1guid;
            someAccount1.Money = 2000;
            accRepo.AddAccount(someAccount1);

            Account someAccount2 = new Account();
            Guid acc2guid = new Guid();
            someAccount2.Id = acc2guid;
            someAccount2.Money = 4000;
            accRepo.AddAccount(someAccount2);*/
            /////////////

            /*Console.ReadLine();
            Console.WriteLine("Transfering money");
            // transfering money
            transferer.TransferMoney(acc1guid, acc2guid, 1000);*/
            

            Console.ReadLine();
            UnregisterService(serviceRepo);
            Console.ReadLine();
        }


        private static void OpenService(Contracts.ICanTransferMoney service)
        {
            log.Info("Opening service endpoint");
            var sh = new ServiceHost(service, new Uri[] { new Uri(SERVICE_ADDRESS) });
            NetTcpBinding serverBinding = new NetTcpBinding(SecurityMode.None);
            sh.AddServiceEndpoint(typeof(Contracts.ICanTransferMoney), serverBinding, SERVICE_ADDRESS);
            sh.Open();
            log.Info("Service endpoint opened");
        }

        private static void RegisterService(IServiceRepository serviceRepo)
        {
            log.Info("Registering service in ServiceRepository");
            
            serviceRepo.RegisterService("ICanTransferMoney", SERVICE_ADDRESS);

            log.Info("Service registered");
            
            AliveKeeper keeper = new AliveKeeper(serviceRepo);
            keepAliveTimer = new Timer(3000);
            keepAliveTimer.Elapsed += new ElapsedEventHandler(keeper.KeepAlive);
            keepAliveTimer.Enabled = true;
        }

        private static void UnregisterService(IServiceRepository serviceRepo)
        {
            log.Info("Unregistering service");
            serviceRepo.Unregister("ICanTransferMoney");
            keepAliveTimer.Enabled = false;
            log.Info("Service unregistered");
        }
    }
}
