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
        public bool AddAudit(Guid accountId, long Money)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Audit> AuditAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Audit> GetAuditsByAccount(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Audit> GetAuditsByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfCredits()
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfTransfersByAccount(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfTransfersByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public int GetTransferedMoneyByAccount(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public int GetTransferedMoneyByDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
