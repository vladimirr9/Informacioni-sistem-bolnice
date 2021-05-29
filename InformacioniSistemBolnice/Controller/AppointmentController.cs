using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    class AppointmentController
    {
        private AppointmentService _appointmentService = new AppointmentService();

        public void Add(Termin appointment)
        {
            _appointmentService.Add(appointment);
        }

        public void Remove(Termin appointment)
        {
            _appointmentService.Remove(appointment);
        }

        public void Update(Termin appointment)
        {
            _appointmentService.Update(appointment);
        }

        public int GenerateNewId()
        {
            return _appointmentService.GenerateNewId();
        }

        public List<Termin> GetAll()
        {
            return _appointmentService.GetAll();
        }

        public Termin GetOne(Termin appointment)
        {
            return _appointmentService.GetOne(appointment);
        }
    }
}
