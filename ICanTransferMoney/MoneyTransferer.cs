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
        private IAuditorService auditorService;

        public MoneyTransferer(IServiceFactory serviceFactory)
        {
            accountRepository = serviceFactory.GetAccountRepository();
            auditorService = serviceFactory.GetAuditorService();
        }


        public bool TransferMoney(Guid accIdFrom, Guid accIdTo, long amount)
        {
            
            bool withdrawn = accountRepository.ChangeAccountBalance(accIdFrom, -amount);
            
            if(withdrawn)
            {
                bool transferred = accountRepository.ChangeAccountBalance(accIdTo, amount);
                if(!transferred)
                {
                    // assert IAccountRepository is FUCKED UP
                    // try to recover
                    accountRepository.ChangeAccountBalance(accIdFrom, amount);
                    return false;
                }
                // make audits
                bool auditFromDone = auditorService.AddAudit(accIdFrom, -amount);
                if (!auditFromDone)
                    scheduleAuditRetry(accIdFrom, -amount);
                bool auditToDone = auditorService.AddAudit(accIdTo, amount);
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
