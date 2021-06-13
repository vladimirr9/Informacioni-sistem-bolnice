using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Patient_ns.Filters
{
    public class PatientFilterByName : PatientFilter
    {
        public bool FitsFilter(Patient patient, string filter)
        {
            return (patient.Name.ToUpper().StartsWith(filter.ToUpper()));
        }
    }
}
