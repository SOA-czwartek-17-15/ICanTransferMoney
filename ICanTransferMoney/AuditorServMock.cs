using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
namespace ICanTransferMoney.Mock
{
    class AuditorServMock : IAuditorService
    {

        bool IAuditorService.AddAudit(string accountNumber, long Money)
        {
            Console.WriteLine("Audit done for accountNr " + accountNumber + " with change of balance " + Money);
            return true;
        }

        IEnumerable<Audit> IAuditorService.AuditAll()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Audit> IAuditorService.GetAuditsByAccount(string accountNumber)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Audit> IAuditorService.GetAuditsByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        int IAuditorService.GetNumberOfCredits()
        {
            throw new NotImplementedException();
        }

        int IAuditorService.GetNumberOfTransfersByAccount(string accountNumber)
        {
            throw new NotImplementedException();
        }

        int IAuditorService.GetNumberOfTransfersByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        int IAuditorService.GetTransferedMoneyByAccount(string accountNumber)
        {
            throw new NotImplementedException();
        }

        int IAuditorService.GetTransferedMoneyByDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
