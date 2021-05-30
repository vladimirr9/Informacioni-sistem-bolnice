using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Service
{
    public class PatientService
    {
        private ActivityLogService _activityLogService = new ActivityLogService();

        public void Register(Pacijent patient)
        {
            if (!IsUsernameUnique(patient.korisnickoIme))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni",
                    MessageBoxButton.OK);
                return;
            }

            if (!IsJMBGUnique(patient.jmbg))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }

            PacijentFileStorage.AddPacijent(patient);
        }

        public void Remove(Pacijent patient)
        {
            PacijentFileStorage.RemovePacijent(patient.korisnickoIme);
        }

        public void Unban(Pacijent patient)
        {
            patient.Banovan = false;
            PacijentFileStorage.UpdatePacijent(patient.korisnickoIme, patient);
        }

        public void RemoveAllergen(Pacijent patient, Ingredient allergen)
        {
            patient.zdravstveniKarton.RemoveAlergen(allergen);
            PacijentFileStorage.UpdatePacijent(patient.korisnickoIme, patient);
        }

        public void AddAllergen(Pacijent patient, Ingredient allergen)
        {
            patient.zdravstveniKarton.AddAllergen(allergen);
            PacijentFileStorage.UpdatePacijent(patient.korisnickoIme, patient);
        }

        public void Update(string initialUsername, Pacijent patient)
        {
            Pacijent initialPatient = PacijentFileStorage.GetOne(initialUsername);
            if (!(IsUsernameUnique(patient.korisnickoIme) || patient.korisnickoIme.Equals(initialUsername)))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni",
                    MessageBoxButton.OK);
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

        public Boolean CheckStatusOfPatient(Pacijent patient)
        {
            Boolean IsBlocked = false;
            int numberOfMakingAppointment = _activityLogService.NumberOfActivity(patient.korisnickoIme, TypeOfActivity.makingAppointment);
            int numberOfEditingAppointment = _activityLogService.NumberOfActivity(patient.korisnickoIme, TypeOfActivity.editingAppointment);
            int numberOfCancelingAppointment = _activityLogService.NumberOfActivity(patient.korisnickoIme, TypeOfActivity.cancelingAppointment);

            if (numberOfMakingAppointment > 3 || numberOfCancelingAppointment > 2 || numberOfEditingAppointment > 2)
            {
                BanPatient(patient);
                IsBlocked = true;
            }

            return IsBlocked;

        }

        private void BanPatient(Pacijent patient)
        {
            foreach (Pacijent p in PacijentFileStorage.GetAll())
            {
                if (p.korisnickoIme.Equals(patient.korisnickoIme))
                {
                    SetInformationsAboutBanning(patient);
                }
            }
        }

        private void SetInformationsAboutBanning(Pacijent patient)
        {
            patient.Banovan = true;
            patient.TrenutakBanovanja = DateTime.Now;
            PacijentFileStorage.UpdatePacijent(patient.korisnickoIme, patient);
        }

        public List<Therapy> GetTherapiesFromRecord(Pacijent patient)
        {
            List<Therapy> therapies = new List<Therapy>();
            foreach (Therapy t in GetMedicalRecordForPatient(patient).Terapija)
            {
                if (t.BeginningDate < DateTime.Now && t.EndingDate > DateTime.Now)
                {
                    therapies.Add(t);
                }
            }
            return therapies;
        }

        private ZdravstveniKarton GetMedicalRecordForPatient(Pacijent patient)
        {
            ZdravstveniKarton medicalRecord = new ZdravstveniKarton();
            foreach (Pacijent p in PacijentFileStorage.GetAll())
            {
                if (p.korisnickoIme.Equals(patient.korisnickoIme))
                {
                    medicalRecord = p.zdravstveniKarton;
                }
            }
            return medicalRecord;
        }
    }
}
