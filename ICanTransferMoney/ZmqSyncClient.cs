using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ZMQ;
using System.Runtime.CompilerServices;

namespace ICanTransferMoney
{
    class ZmqSyncClient
    {
        private readonly string _ipAddress;

        private Context _context;
        private Socket _clientSocket;
        private bool _open = false;

        public ZmqSyncClient(string ipAddress)
        {
            _ipAddress = ipAddress;
            _context = new Context();
            _clientSocket = _context.Socket(SocketType.REQ);
            _clientSocket.Connect(_ipAddress);
            _open = true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string SendAndGetResponse(string request)
        {
            if (!_open)
                return null;

            _clientSocket.Send(request, Encoding.Unicode);
            return _clientSocket.Recv(Encoding.Unicode, 10000);
        }

        public void Dispose()
        {
            _open = false;
            _clientSocket.Dispose();
            _context.Dispose();
        }
    }
}
