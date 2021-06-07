using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Doctor_ns;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    class HospitalisationService
    {
        public void Add(Hospitalisation hospitalisation)
        {
            HospitalisationFileRepository.AddHospitalisation(hospitalisation);
        }

        public void Remove(Hospitalisation hospitalisation)
        {
            HospitalisationFileRepository.RemoveHospitalisation(hospitalisation.HospitalisationId);
        }

        public void Update(Hospitalisation hospitalisation)
        {
            HospitalisationFileRepository.UpdateHospitalisation(hospitalisation.HospitalisationId, hospitalisation);
        }

        public List<Hospitalisation> GetAll()
        {
            return HospitalisationFileRepository.GetAll();
        }

        public Hospitalisation GetHospitalisationForPatient(Patient patient)
        {
            foreach (Hospitalisation hospitalisation in HospitalisationFileRepository.GetAll())
            {
                if (hospitalisation.PatientUsername.Equals(patient.Username) && hospitalisation.EndDate > DateTime.Now)
                {
                    return hospitalisation;
                }
            }

            return null;
        }

        public int GenerateNewId()
        {
            return HospitalisationFileRepository.GetAll().Count + 1;
        }

        public void Save(Hospitalisation hospitalisation)
        {
            foreach (Hospitalisation hosp in HospitalisationFileRepository.GetAll())
            {
                if (hosp.HospitalisationId.Equals(hospitalisation.HospitalisationId))
                {
                    HospitalisationFileRepository.UpdateHospitalisation(hospitalisation.HospitalisationId, hospitalisation);
                    return;
                }
            }

            HospitalisationFileRepository.AddHospitalisation(hospitalisation);
        }
    }
}
