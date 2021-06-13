using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Manager_ns.Strategy
{
    public class ContextClass
    {
        private IStrategy _strategy;
        public ContextClass() { }
        public ContextClass(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        public void DoRenovation(object firstObject, object secondObjct)
        {
            this._strategy.DoRenovate(firstObject, secondObjct);
        }
    }
}
