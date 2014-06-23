using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Contracts;
using Newtonsoft.Json;

namespace ICanTransferMoney
{
    class ServiceRepository
    {
        private ZmqSyncClient _zmq;
        private Thread _aliveThread;
        private bool _open = false;

        public ServiceRepository(string ipAddress)
        {
            _zmq = new ZmqSyncClient(ipAddress);
            _open = true;
            Console.WriteLine("Connecting to service repo on: " + ipAddress);
        }

        internal ServiceLocations GetServiceLocations()
        {
            ServiceLocations serviceLocs = new ServiceLocations();

            JSONMessage msg = new JSONMessage();
            msg.Function = "getServiceAddress";
            msg.Service = "IAccountRepository";

            serviceLocs.accountRepoAddress = _zmq.SendAndGetResponse(JsonConvert.SerializeObject(msg));

            msg.Service = "IAuditorService";
            serviceLocs.auditorAddress = _zmq.SendAndGetResponse(JsonConvert.SerializeObject(msg));
            return serviceLocs;
        }

        internal bool RegisterMe()
        {
            JSONMessage msg = new JSONMessage();
            msg.Function = "registerService";
            msg.Service = "ICanTransferMoney";

            string response = _zmq.SendAndGetResponse(JsonConvert.SerializeObject(msg));
            Console.WriteLine("Registration response: " + response);

            return true; //FIXME
        }

        internal void UnregisterMe()
        {
            JSONMessage msg = new JSONMessage();
            msg.Function = "unregisterService";
            msg.Service = "ICanTransferMoney";

            string response = _zmq.SendAndGetResponse(JsonConvert.SerializeObject(msg));
            Console.WriteLine("Unregistration response: " + response);
        }

        internal void Dispose()
        {
            _zmq.Dispose();
        }

        internal void sustain()
        {
            _aliveThread = new Thread(SendAlive);
            //_aliveThread.Start();
        }

        private void SendAlive()
        {
            while (_open)
            {
                JSONMessage msg = new JSONMessage();
                msg.Function = "isAlive";
                msg.Service = "ICanTransferMoney";
                Console.WriteLine("Sending alive");
                _zmq.SendAndGetResponse(JsonConvert.SerializeObject(msg));
                Console.WriteLine("Alive sent");
                Thread.Sleep(3000);
            }
        }
    }
}
