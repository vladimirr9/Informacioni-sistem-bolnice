using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Doctor_ns;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IHospitalisationRepository
    {
        List<Hospitalisation> GetAll();
        Hospitalisation GetOne(int hospitalisationId);
        Boolean Remove(int hospitalisationId);
        Boolean Add(Hospitalisation newHospitalisation);
        Boolean Update(int hospitalisationId, Hospitalisation newHospitalisation);
    }
}
