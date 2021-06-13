using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    public interface IHospitalisation
    {
        List<Room> GetRoomsForHospitalisation(DateTime begin, DateTime end);
        int GetAvailableBed(Room room, DateTime begin, DateTime end);

    }
}
