using InformacioniSistemBolnice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Controller
{
    public class DoctorsAppointmentController
    {
        private DoctorsAppointmentService _doctorsAppointmentService = new DoctorsAppointmentService();
        public void UpdateAppointmentsForDoctor(Doctor doctor)
        {
            _doctorsAppointmentService.UpdateAppointmentsForDoctor(doctor);
        }
    }
}
