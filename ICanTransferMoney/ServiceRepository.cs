using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Newtonsoft.Json;

namespace ICanTransferMoney
{
    class ServiceRepository
    {
        private ZmqSyncClient zmq;

        public ServiceRepository(string ipAddress)
        {
            zmq = new ZmqSyncClient(ipAddress);
        }

        internal ServiceLocations GetServiceLocations()
        {
            ServiceLocations serviceLocs = new ServiceLocations();

            JSONMessage msg = new JSONMessage();
            msg.Function = "getServiceAddress";
            msg.Service = "IAccountRepository";

            serviceLocs.accountRepoAddress = zmq.SendAndGetResponse(JsonConvert.SerializeObject(msg));

            msg.Service = "IAuditorService";
            serviceLocs.auditorAddress = zmq.SendAndGetResponse(JsonConvert.SerializeObject(msg));
            return serviceLocs;
        }

        internal bool RegisterMe()
        {
            JSONMessage msg = new JSONMessage();
            msg.Function = "registerService";
            msg.Service = "ICanTransferMoney";

            string response = zmq.SendAndGetResponse(JsonConvert.SerializeObject(msg));
            Console.WriteLine("Registration response: " + response);

            return true; //FIXME
        }

        internal void UnregisterMe()
        {
            JSONMessage msg = new JSONMessage();
            msg.Function = "unregisterService";
            msg.Service = "ICanTransferMoney";

            string response = zmq.SendAndGetResponse(JsonConvert.SerializeObject(msg));
            Console.WriteLine("Unregistration response: " + response);
        }

        internal void Dispose()
        {
            zmq.Dispose();
        }
    }
}
