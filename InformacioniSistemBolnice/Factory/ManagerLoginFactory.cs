using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Factory
{
    class ManagerLoginFactory : LoginFactory
    {
        private Manager _manager;

        public ManagerLoginFactory(Manager manager)
        {
            this._manager = manager;
        }
        public override ILoginer GetLoginer()
        {
            return new ManagerLoginer(this._manager);
        }
    }
}
