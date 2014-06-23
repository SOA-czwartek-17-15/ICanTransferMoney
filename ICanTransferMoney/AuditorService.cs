using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICanTransferMoney
{
    class AuditorService : IDisposable
    {
        private string _ipAddress;

        public AuditorService(string ipAddress)
        {
            _ipAddress = ipAddress;
        }


       public void Dispose()
        {
            
        }
    }
}
