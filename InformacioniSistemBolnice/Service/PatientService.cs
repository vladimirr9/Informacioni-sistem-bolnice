﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Service
{
    public class PatientService
    {
        public void Register(Pacijent patient)
        {
            if (!IsUsernameUnique(patient.korisnickoIme))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            if (!IsJMBGUnique(patient.jmbg))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            PacijentFileStorage.AddPacijent(patient);
        }

        internal void Unban(Pacijent patient)
        {
            patient.Banovan = false;
            PacijentFileStorage.UpdatePacijent(patient.korisnickoIme, patient);
        }

        public void Update(string initialUsername, Pacijent patient)
        {
            Pacijent initialPatient = PacijentFileStorage.GetOne(initialUsername);
            if (!(IsUsernameUnique(patient.korisnickoIme) || patient.korisnickoIme.Equals(initialUsername)))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            if (!(IsJMBGUnique(patient.jmbg) || patient.jmbg.Equals(initialPatient.jmbg)))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }

            if (!initialUsername.Equals(patient.korisnickoIme))
                UpdateAppointmentsForUsernameChange(patient.korisnickoIme, initialUsername);
            PacijentFileStorage.UpdatePacijent(initialUsername, patient);


        }






        public bool IsUsernameUnique(String username)
        {
            if (PacijentFileStorage.GetOne(username) == null)
                return true;
            return false;
        }

        public bool IsJMBGUnique(String jmbg)
        {
            if (PacijentFileStorage.GetOneByJMBG(jmbg) == null)
                return true;
            return false;
        }

        private void UpdateAppointmentsForUsernameChange(string username, string initialUsername)
        {
            foreach (Termin appointment in TerminFileStorage.GetAll())
            {
                if (appointment.KorisnickoImePacijenta.Equals(initialUsername))
                {
                    appointment.KorisnickoImePacijenta = username;
                    TerminFileStorage.UpdateTermin(appointment.iDTermina, appointment);
                }
            }
        }
    }
}
