using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Contracts;

namespace ICanTransferMoney
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MoneyTransferer : Contracts.ICanTransferMoney
    {
        private IServiceFactory serviceFactory;

        public MoneyTransferer(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public bool TransferMoney(Guid accIdFrom, Guid accIdTo, long amount)
        {
            IAccountRepository accountRepository = serviceFactory.GetAccountRepository();
            IAuditorService auditorService = serviceFactory.GetAuditorService();

            
            string accNrFrom = accountRepository.GetAccountById(accIdFrom).AccountNumber;
            string accNrTo = accountRepository.GetAccountById(accIdTo).AccountNumber;


            // OPERATIONS:
            bool withdrawn = accountRepository.ChangeAccountBalance(accIdFrom, -amount);
            
            if(withdrawn)
            {
                bool transferred = accountRepository.ChangeAccountBalance(accIdTo, amount);
                if(!transferred)
                {
                    // assert IAccountRepository is BROKEN
                    // try to recover
                    accountRepository.ChangeAccountBalance(accIdFrom, amount);
                    return false;
                }
                // make audits
                bool auditFromDone = auditorService.AddAudit(accNrFrom,-amount);
                if (!auditFromDone)
                    scheduleAuditRetry(accIdFrom, -amount);
                bool auditToDone = auditorService.AddAudit(accNrTo,amount);
                if (!auditToDone)
                    scheduleAuditRetry(accIdTo, amount);
                return true;
            }
            
            // probably not enough money
            return false;
        }



        public void scheduleAuditRetry(Guid accountId, long amount)
        {
            throw new NotImplementedException();
        }
    }
}
