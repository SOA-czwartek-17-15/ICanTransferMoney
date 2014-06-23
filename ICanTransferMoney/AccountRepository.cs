using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using Newtonsoft.Json;

namespace ICanTransferMoney
{
    public class AccountRepository : IDisposable
    {
        private ZmqSyncClient zmq;

        public AccountRepository(string address)
        {
            zmq = new ZmqSyncClient(address);
        }

        public Account GetAccount(string accountNr)
        {
            JSONMessage msg = new JSONMessage();
            msg.Function = "GetAccountInformation";
            msg.Parameters = new String[1];
            msg.Parameters[0] = accountNr;
            string response = zmq.SendAndGetResponse(JsonConvert.SerializeObject(msg));
            return JsonConvert.DeserializeObject<Account>(response);
        }

        public void ChangeAccountBalance(Guid accountId,long amount)
        {
            JSONMessage msg = new JSONMessage();
            msg.Function = "ChangeAccountBalance";
            msg.Parameters = new String[2];
            msg.Parameters[0] = accountId.ToString();
            msg.Parameters[1] = amount.ToString();
            string response = zmq.SendAndGetResponse(JsonConvert.SerializeObject(msg));
            // FIXME get response
        }

        public void Dispose()
        {
            zmq.Dispose();
        }
    }
}
