using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Secretary_ns;

namespace InformacioniSistemBolnice.Factory
{
    class SecretaryLoginFactory : LoginFactory
    {
        private Secretary _secretary;

        public SecretaryLoginFactory(Secretary secretary)
        {
            this._secretary = secretary;
        }
        public override Loginer GetLoginer()
        {
            return new SecretaryLoginer(this._secretary);
        }
    }
}
