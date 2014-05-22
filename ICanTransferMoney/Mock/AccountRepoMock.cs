using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
namespace ICanTransferMoney.Mock
{
    class AccountRepoMock : IAccountRepository
    {
        public bool ChangeAccountBalance(Guid accountId, long amount)
        {
            throw new NotImplementedException();
        }

        public long CreateAccount(int clientId, Account details)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountById(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountInformation(string accountNumber)
        {
            throw new NotImplementedException();
        }
    }
}
