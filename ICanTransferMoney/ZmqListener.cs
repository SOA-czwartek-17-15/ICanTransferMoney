using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZMQ;

namespace ICanTransferMoney
{
    class ZmqListener
    {
        private readonly string _ipAddress;
        private readonly int _port;
        private Thread _workerThread;
        private object _locker = new object();
        private bool _stop;

        public ZmqListener(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        private void RunZmqClient()
        {
            using(var context = new Context())
            using (var server = context.Socket(SocketType.REP))
            {
                var bindingAddress = new StringBuilder("tcp://");
                bindingAddress.Append(_ipAddress);
                bindingAddress.Append(":");
                bindingAddress.Append(_port);

                server.Bind(bindingAddress.ToString());

                while (!_stop)
                {
                    string message = server.Recv(Encoding.Unicode, 100);
                    if(message==null)
                    {
                        continue;
                    }

                    var response = ProcessMessage(message);
                }
            }
        }

        private string ProcessMessage(string message)
        {
            throw new NotImplementedException();
        }

        private void Start()
        {
            _workerThread = new Thread(RunZmqClient);
            _workerThread.Start();
        }

        private void Stop()
        {
            lock (_locker)
            {
                _stop = true;
            }
            _workerThread.Join();
        }
    }
}
