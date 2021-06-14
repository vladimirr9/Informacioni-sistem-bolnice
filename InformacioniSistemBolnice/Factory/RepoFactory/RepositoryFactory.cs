using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.Secretary_ns;

namespace InformacioniSistemBolnice.Factory.RepoFactory
{
    abstract class RepositoryFactory
    {
        public abstract IDoctorRepository GetDoctorRepository();
        public abstract IPatientRepository GetPatientRepository();
        public abstract IManagerRepository GetManagerRepository();
        public abstract ISecretaryRepository GetSecretaryRepository();
    }
}
