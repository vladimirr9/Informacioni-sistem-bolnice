using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IPatientRepository : IUserRepository<Patient>
    {
        Patient GetOneByJMBG(string jmbg);
    }
}
