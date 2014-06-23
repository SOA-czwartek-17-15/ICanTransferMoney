using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;

namespace ICanTransferMoney
{
    class MoneyTransferService : IDisposable
    {
        private AccountRepository _accountRepo;
        private AuditorService _auditorServ;
        private bool _open = false;

        public MoneyTransferService(AccountRepository accountRepo, AuditorService auditorServ)
        {
            _accountRepo = accountRepo;
            _auditorServ = auditorServ;
        }

        public bool TransferMoney(string fromAccNr, string toAccNr, long amount)
        {
            if (!_open)
                return false;

            Account from = _accountRepo.GetAccount(fromAccNr);
            Account to = _accountRepo.GetAccount(toAccNr);

            long newBalanceFrom = from.Money - amount;
            
            if (newBalanceFrom < 0)
                return false;

            _accountRepo.ChangeAccountBalance(from.Id, -amount);
            _accountRepo.ChangeAccountBalance(to.Id, amount);

            return true;
        }

        public void Dispose()
        {
            _open = false;
            _accountRepo.Dispose();
            _auditorServ.Dispose();
        }
    }
}
