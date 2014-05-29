using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Contracts;
using log4net;
namespace ICanTransferMoney
{
    class AliveKeeper
    {
        private IServiceRepository servRepo;
        private static readonly ILog log = LogManager.GetLogger(typeof(AliveKeeper));

        public AliveKeeper(IServiceRepository servRepo)
        {
            this.servRepo = servRepo;
        }

        public void KeepAlive(object source, ElapsedEventArgs args)
        {
            log.Info("Sending alive");
            servRepo.Alive("ICanTransferMoney");
            log.Info("Alive sent");
        }
    }
}
