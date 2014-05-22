using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Contracts;
namespace ICanTransferMoney
{
    class AliveKeeper
    {
        private IServiceRepository servRepo;
        
        public AliveKeeper(IServiceRepository servRepo)
        {
            this.servRepo = servRepo;
        }

        public void KeepAlive(object source, ElapsedEventArgs args)
        {
            servRepo.Alive("ICanTransferMoney");
            Console.WriteLine("KeepAlive sent");
        }
    }
}
