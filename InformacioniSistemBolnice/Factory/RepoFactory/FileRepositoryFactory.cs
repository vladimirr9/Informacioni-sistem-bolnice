using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.Secretary_ns;

namespace InformacioniSistemBolnice.Factory.RepoFactory
{
    class FileRepositoryFactory : RepositoryFactory
    {
        public override IDoctorRepository GetDoctorRepository()
        {
            return new DoctorFileRepository();
        }

        public override IPatientRepository GetPatientRepository()
        {
            return new PatientFileRepository();
        }

        public override IManagerRepository GetManagerRepository()
        {
            return new ManagerFileRepository();
        }

        public override ISecretaryRepository GetSecretaryRepository()
        {
            return new SecretaryFileRepository();
        }
    }
}
