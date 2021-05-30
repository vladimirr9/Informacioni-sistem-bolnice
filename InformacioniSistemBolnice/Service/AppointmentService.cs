﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    public class AppointmentService
    {
        public void Add(Termin appointment)
        {
            //odraditi provere!
            TerminFileStorage.AddTermin(appointment);
        }

        public void Remove(Termin appointment)
        {
            TerminFileStorage.RemoveTermin(appointment.iDTermina);
        }

        public void Update(Termin appointment)
        {
            //odraditi provere!
            TerminFileStorage.UpdateTermin(appointment.iDTermina, appointment);
        }

        public List<Termin> GetAll()
        {
            return TerminFileStorage.GetAll();
        }

        public int GenerateNewId()
        {
            return TerminFileStorage.GetAll().Count + 1;
        }

        public Termin GetOne(Termin appointment)
        {
            return TerminFileStorage.GetOne(appointment.iDTermina);
        }
    }
}