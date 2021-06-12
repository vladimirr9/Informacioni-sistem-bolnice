using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Factory
{
    abstract class LoginFactory
    {
        public abstract Loginer GetLoginer();
    }
}
