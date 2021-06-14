using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Patient_ns.State
{

    public class ContextClassForStates
    {
        public IStatusOfPatient CurrentState;
        public IStatusOfPatient Banned { get; set; }
        public IStatusOfPatient Unbanned { get; set; }

        public ContextClassForStates(Patient p)
        {
            Banned = new Banned();
            Unbanned = new Unbanned();
            CurrentState = SetCurrentStateForPatient(p);

        }

        public IStatusOfPatient SetCurrentStateForPatient(Patient p)
        {
            IStatusOfPatient status = Unbanned;
            if (p.Banned == true)
            {
                status = Banned;
            }

            return status;
        }
    }
}
