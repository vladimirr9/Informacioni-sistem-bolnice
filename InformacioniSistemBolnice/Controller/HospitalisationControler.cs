using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Doctor_ns;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    class HospitalisationControler
    {
        private HospitalisationService _hospitalisationService = new HospitalisationService();

        public void Add(Hospitalisation hospitalisation)
        { 
            _hospitalisationService.Add(hospitalisation);
        }

        public void Remove(Hospitalisation hospitalisation)
        {
            _hospitalisationService.Remove(hospitalisation);
        }

        public void Update(Hospitalisation hospitalisation)
        {
            _hospitalisationService.Update(hospitalisation);
        }

        public List<Hospitalisation> GetAll()
        {
            return _hospitalisationService.GetAll();
        }

        public Hospitalisation GetHospitalisationForPatient(Patient patient)
        {
            return _hospitalisationService.GetHospitalisationForPatient(patient);
        }

        public int GenerateNewId()
        {
            return _hospitalisationService.GenerateNewId();
        }

        public void Save(Hospitalisation hospitalisation)
        {
            _hospitalisationService.Save(hospitalisation);
        }
    }
}
