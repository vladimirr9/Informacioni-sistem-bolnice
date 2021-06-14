using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Controller;

namespace InformacioniSistemBolnice.Patient_ns.State
{
    public class Banned : IStatusOfPatient
    {
        public ContextClassForStates context { get; set; }
        public BanningPatientController _banningPatientController { get; }
        public UnbanningPatientController _unbanningPatientController { get; }
        public void TransitionBetweenStates(Patient patient, ContextClassForStates ctx)
        {
            throw new NotImplementedException();
        }
    }
}
