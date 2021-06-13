using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Patient_ns.Filters
{
    public interface PatientFilter
    {
        bool FitsFilter(Patient patient, string filter);
    }
}
