using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Contracts;

namespace ICanTransferMoney
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MoneyTransferer : Contracts.ICanTransferMoney
    {
        private IAccountRepository accountRepository;

        public MoneyTransferer(IServiceRepository serviceRepo)
        {
            string address = serviceRepo.GetServiceAddress("IAccountRepository");
            NetTcpBinding srBinding = new NetTcpBinding();
            ChannelFactory<IAccountRepository> cf = new ChannelFactory<IAccountRepository>(srBinding, new EndpointAddress(address));
            accountRepository = cf.CreateChannel();
        }


        public bool TransferMoney(string accNrFrom, string accNrTo, long amount)
        {
            // ZARYS
            // - Pobierz konta
            // - Przelej pieniądze
            // - Zapisz przelew do BD
            // - Zamknij połączenie

            Account accFrom = accountRepository.GetAccountInformation(accNrFrom);
            Account accTo = accountRepository.GetAccountInformation(accNrTo);

            if (accFrom.Money < amount)
                return false;

            //TODO

            Console.Out.WriteLine("Not implemented yet");
            return false;
        }




    }
}
