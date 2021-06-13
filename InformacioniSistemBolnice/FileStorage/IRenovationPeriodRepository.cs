using InformacioniSistemBolnice.Manager_ns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IRenovationPeriodRepository
    {
        List<RenovationPeriod> GetAll();

        RenovationPeriod GetOne(int roomId);

        Boolean Remove(int roomId);

        Boolean Add(RenovationPeriod newRenovationPeriod);

        Boolean Update(int roomId, RenovationPeriod newRenovationPeriod);
    }
}
