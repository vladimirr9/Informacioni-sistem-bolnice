using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class UnbanningPatientService
    {
        private IPatientRepository _patientRepository = new PatientFileRepository();
        public void Unban(Patient patient)
        {
            patient.Banned = false;
            _patientRepository.Update(patient.Username, patient);
        }
    }
}
