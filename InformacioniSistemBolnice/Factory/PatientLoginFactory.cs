using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Factory
{
    class PatientLoginFactory : LoginFactory
    {
        private Patient _patient;

        public PatientLoginFactory(Patient patient)
        {
            this._patient = patient;
        }
        public override Loginer GetLoginer()
        {
            return new PatientLoginer(this._patient);
        }
    }
}
