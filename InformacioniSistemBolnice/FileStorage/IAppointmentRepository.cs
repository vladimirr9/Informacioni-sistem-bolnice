using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAll();
        Appointment GetOne(int appointmentID);
        Boolean Remove(int appointmentID);
        Boolean Add(Appointment newAppointment);
        Boolean Update(int appointmentID, Appointment newAppointment);
    }
}
