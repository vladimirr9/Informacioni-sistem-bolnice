using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class UnbanningPatientController
    {
        private UnbanningPatientService _unbanningPatientService = new UnbanningPatientService();

        public void Unban(Patient patient)
        {
            _unbanningPatientService.Unban(patient);
        }
    }
}
