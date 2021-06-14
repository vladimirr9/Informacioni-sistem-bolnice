using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class BanningPatientController
    {
        private BanningPatientService _banningPatientService = new BanningPatientService();
        public Boolean CheckStatusOfPatient(Patient patient)
        {
            return _banningPatientService.CheckStatusOfPatient(patient);
        }
    }
}
