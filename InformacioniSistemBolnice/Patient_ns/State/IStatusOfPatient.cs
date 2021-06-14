using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Controller;

namespace InformacioniSistemBolnice.Patient_ns.State
{
    public interface IStatusOfPatient
    {
        ContextClassForStates context { get; set; }
        BanningPatientController _banningPatientController { get; }
        UnbanningPatientController _unbanningPatientController { get; }
        void TransitionBetweenStates(Patient patient,ContextClassForStates ctx);


    }
}
