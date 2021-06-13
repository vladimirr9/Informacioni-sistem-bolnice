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
        private IHospitalisationRepository _hospitalisationFileRepository = new HospitalisationFileRepository();
        public void Add(Hospitalisation hospitalisation)
        {
            _hospitalisationFileRepository.Add(hospitalisation);
        }

        public void Remove(Hospitalisation hospitalisation)
        {
            _hospitalisationFileRepository.Remove(hospitalisation.HospitalisationId);
        }

        public void Update(Hospitalisation hospitalisation)
        {
            _hospitalisationFileRepository.Update(hospitalisation.HospitalisationId, hospitalisation);
        }

        public List<Hospitalisation> GetAll()
        {
            return _hospitalisationFileRepository.GetAll();
        }

        public Hospitalisation GetHospitalisationForPatient(Patient patient)
        {
            foreach (Hospitalisation hospitalisation in _hospitalisationFileRepository.GetAll())
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
            return _hospitalisationFileRepository.GetAll().Count + 1;
        }

        public void Save(Hospitalisation hospitalisation)
        {
            foreach (Hospitalisation hosp in _hospitalisationFileRepository.GetAll())
            {
                if (hosp.HospitalisationId.Equals(hospitalisation.HospitalisationId))
                {
                    _hospitalisationFileRepository.Update(hospitalisation.HospitalisationId, hospitalisation);
                    return;
                }
            }

            _hospitalisationFileRepository.Add(hospitalisation);
        }
    }
}
