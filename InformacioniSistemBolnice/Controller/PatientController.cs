using InformacioniSistemBolnice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Controller
{
    
    public class PatientController
    {
        private PatientService _patientService = new PatientService();


        public void Register(Pacijent patient)
        {
            _patientService.Register(patient);
        }

        public void Update(string initialUsername, Pacijent patient)
        {
            _patientService.Update(initialUsername, patient);
        }

        public void Unban(Pacijent patient)
        {
            _patientService.Unban(patient);
        }
    }
}
