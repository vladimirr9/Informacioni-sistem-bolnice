using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Patient_ns;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IActivityLogRepository
    {
        List<ActivityLog> GetAll();
        Boolean Add(ActivityLog newActivity);
    }
}
