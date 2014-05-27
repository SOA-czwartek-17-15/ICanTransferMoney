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
        List<Account> accounts;

        public AccountRepoMock()
        {
            accounts = new List<Account>();
        }

        public bool ChangeAccountBalance(Guid accountId, long amount)
        {
            foreach(var acc in accounts){
                if(acc.Id.Equals(accountId))
                {
                    if (acc.Money + amount < 0)
                        return false;
                    Console.WriteLine("Changing balance of accId=" + acc.Id + " by "+amount);
                    acc.Money += amount;
                    return true;
                }
            }
            return false;
        }


        public Account GetAccountById(Guid accountId)
        {
            foreach (var acc in accounts)
            {
                if (acc.ClientId.Equals(accountId))
                {
                    return acc;
                }
            }
            return null;
        }

        public Account GetAccountInformation(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public void AddAccount(Account acc)
        {
            accounts.Add(acc);
        }


        bool IAccountRepository.CreateAccount(Account details)
        {
            throw new NotImplementedException();
        }
    }
}
