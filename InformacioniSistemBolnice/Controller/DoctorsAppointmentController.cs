using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Controller
{
    public class DoctorsAppointmentController
    {
        private DoctorsAppointmentController _doctorsAppointmentController = new DoctorsAppointmentController();
        public void UpdateAppointmentsForDoctor(Doctor doctor)
        {
            _doctorsAppointmentController.UpdateAppointmentsForDoctor(doctor);
        }
    }
}
