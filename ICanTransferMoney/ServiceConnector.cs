﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using System.ServiceModel;

namespace ICanTransferMoney
{
    class ServiceConnector : IServiceFactory
    {
        private static ServiceConnector instance;

        // should be injected
        private string serviceRepositoryAddress { get; set; }

        private IServiceRepository serviceRepository;
        private IAccountRepository accountRepository;
        private IAuditorService auditorService;

        // singleton class
        private ServiceConnector() 
        {
            // only for debug
            serviceRepositoryAddress = "net.tcp://192.168.0.90:50000/IServiceRepository";
        }

        public static ServiceConnector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceConnector();
                }
                return instance;
            }
        }

        public IServiceRepository GetServiceRepository()
        {
            if (serviceRepository != null)
                return serviceRepository;
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            ChannelFactory<IServiceRepository> cf = new ChannelFactory<IServiceRepository>(binding, new EndpointAddress(serviceRepositoryAddress));
            serviceRepository = cf.CreateChannel();
            return serviceRepository;
        }

        public IAccountRepository GetAccountRepository()
        {
            if (accountRepository != null)
                return accountRepository;
            string address = GetServiceRepository().GetServiceLocation("IAccountRepository");
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            ChannelFactory<IAccountRepository> cf = new ChannelFactory<IAccountRepository>(binding, new EndpointAddress(address));
            accountRepository = cf.CreateChannel();
            return accountRepository;
        }

        public IAuditorService GetAuditorService()
        {
            if (auditorService != null)
                return auditorService;
            string address = GetServiceRepository().GetServiceLocation("IAuditorService");
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            ChannelFactory<IAuditorService> cf = new ChannelFactory<IAuditorService>(binding, new EndpointAddress(address));
            auditorService = cf.CreateChannel();
            return auditorService;
        }
    }
}
