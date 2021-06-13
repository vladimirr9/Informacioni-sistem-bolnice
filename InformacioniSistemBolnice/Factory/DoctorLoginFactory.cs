using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Factory
{
    class DoctorLoginFactory : LoginFactory
    {
        private Doctor _doctor;

        public DoctorLoginFactory(Doctor doctor)
        {
            this._doctor = doctor;
        }
        public override ILoginer GetLoginer()
        {
            return new DoctorLoginer(this._doctor);
        }
    }
}
