using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Manager_ns.Strategy
{
    public interface IStrategy
    {
        AppointmentController AppointmentController { get; }
        RoomController RoomController { get; }
        void DoRenovate(object firstObject, object scondObject);
    }
}
