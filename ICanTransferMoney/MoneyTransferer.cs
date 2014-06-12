using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Contracts;
using log4net;
namespace ICanTransferMoney
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MoneyTransferer : Contracts.ICanTransferMoney
    {
        private IServiceFactory serviceFactory;
        private ILog log = LogManager.GetLogger("MoneyTransferer"); 

        public MoneyTransferer(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public bool TransferMoney(Guid accIdFrom, Guid accIdTo, long amount)
        {
            IAccountRepository accountRepository;
            IAuditorService auditorService;

            try
            {
                accountRepository = serviceFactory.GetAccountRepository();
                auditorService = serviceFactory.GetAuditorService();
                if (accountRepository == null || auditorService == null)
                    return false;
            }
            catch(EndpointNotFoundException)
            {
                Console.WriteLine("Cannot connect to required services.");
                return false;
            }

            string accNrFrom;
            string accNrTo;

            try
            {
                accNrFrom = accountRepository.GetAccountById(accIdFrom).AccountNumber;
                accNrTo = accountRepository.GetAccountById(accIdTo).AccountNumber;
            }
            catch(NullReferenceException)
            {
                log.Warn("Account with given Guid doesn't exist. Returning false.");
                return false;
            }

            // OPERATIONS:
            try
            {

                bool withdrawn = accountRepository.ChangeAccountBalance(accIdFrom, -amount);
                log.Info("Money withdrawn from " + accIdFrom);
                if(withdrawn)
                {
                    bool transferred = accountRepository.ChangeAccountBalance(accIdTo, amount);
                    log.Info("Money trasfered to "+accIdTo);
                    if(!transferred)
                    {
                        // assert IAccountRepository is BROKEN
                        // try to recover
                        accountRepository.ChangeAccountBalance(accIdFrom, amount);
                        return false;
                    }
                    // make audits
                    bool auditFromDone = auditorService.AddAudit(accNrFrom,-amount);
                    //if (!auditFromDone)
                    //    scheduleAuditRetry(accIdFrom, -amount);
                    bool auditToDone = auditorService.AddAudit(accNrTo,amount);
                    //if (!auditToDone)
                    //    scheduleAuditRetry(accIdTo, amount);
                    if (!auditFromDone || !auditToDone)
                    {
                        log.Error("Audit failed!");
                    }
                    log.Info("Money trasfered from "+accIdFrom+" to "+accIdTo+", amount="+amount);
                    return true;
                }
                else
                {
                    log.Info("Transfer not possible, not enough money");
                    return false;
                }

            }
            catch (Exception ex)
            {
                log.Error("Transfering error: "+ex.Message);
                return false;
            }

        }



        public void scheduleAuditRetry(Guid accountId, long amount)
        {
            throw new NotImplementedException();
        }
    }
}
