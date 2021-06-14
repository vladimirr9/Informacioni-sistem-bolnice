using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Controller;

namespace InformacioniSistemBolnice.Patient_ns.State
{
    public class Unbanned : IStatusOfPatient
    {
        public ContextClassForStates context { get; set; }
        public BanningPatientController _banningPatientController { get; }
        public UnbanningPatientController _unbanningPatientController { get; }


        public void TransitionBetweenStates(Patient patient,ContextClassForStates ctx)
        {
            if (_banningPatientController.CheckStatusOfPatient(patient) && ctx.CurrentState == )
            {
                ctx.CurrentState = new Banned();
            }
        }
    }
}
