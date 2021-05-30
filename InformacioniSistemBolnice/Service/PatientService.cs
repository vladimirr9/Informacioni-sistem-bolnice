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

        public void Register(Patient patient)
        {
            if (!IsUsernameUnique(patient.Username))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni",
                    MessageBoxButton.OK);
                return;
            }

            if (!IsJMBGUnique(patient.JMBG))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }

            PatientFileRepository.AddPatient(patient);
        }

        public void Remove(Patient patient)
        {
            PatientFileRepository.RemovePatient(patient.Username);
        }

        public void Unban(Patient patient)
        {
            patient.Banned = false;
            PatientFileRepository.UpdatePatient(patient.Username, patient);
        }

        public void RemoveAllergen(Patient patient, Ingredient allergen)
        {
            patient.MedicalRecord.RemoveAlergen(allergen);
            PatientFileRepository.UpdatePatient(patient.Username, patient);
        }

        public void AddAllergen(Patient patient, Ingredient allergen)
        {
            patient.MedicalRecord.AddAllergen(allergen);
            PatientFileRepository.UpdatePatient(patient.Username, patient);
        }

        public void Update(string initialUsername, Patient patient)
        {
            Patient initialPatient = PatientFileRepository.GetOne(initialUsername);
            if (!(IsUsernameUnique(patient.Username) || patient.Username.Equals(initialUsername)))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni",
                    MessageBoxButton.OK);
                return;
            }

            if (!(IsJMBGUnique(patient.JMBG) || patient.JMBG.Equals(initialPatient.JMBG)))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }

            if (!initialUsername.Equals(patient.Username))
                UpdateAppointmentsForUsernameChange(patient.Username, initialUsername);
            PatientFileRepository.UpdatePatient(initialUsername, patient);


        }






        public bool IsUsernameUnique(String username)
        {
            if (PatientFileRepository.GetOne(username) == null)
                return true;
            return false;
        }

        public bool IsJMBGUnique(String jmbg)
        {
            if (PatientFileRepository.GetOneByJMBG(jmbg) == null)
                return true;
            return false;
        }

        private void UpdateAppointmentsForUsernameChange(string username, string initialUsername)
        {
            foreach (Appointment appointment in AppointmentFileRepository.GetAll())
            {
                if (appointment.PatientUsername.Equals(initialUsername))
                {
                    appointment.PatientUsername = username;
                    AppointmentFileRepository.UpdateAppointment(appointment.AppointmentID, appointment);
                }
            }
        }

        public Boolean CheckStatusOfPatient(Patient patient)
        {
            Boolean IsBlocked = false;
            int numberOfMakingAppointment = _activityLogService.NumberOfActivity(patient.Username, TypeOfActivity.makingAppointment);
            int numberOfEditingAppointment = _activityLogService.NumberOfActivity(patient.Username, TypeOfActivity.editingAppointment);
            int numberOfCancelingAppointment = _activityLogService.NumberOfActivity(patient.Username, TypeOfActivity.cancelingAppointment);

            if (numberOfMakingAppointment > 3 || numberOfCancelingAppointment > 2 || numberOfEditingAppointment > 2)
            {
                BanPatient(patient);
                IsBlocked = true;
            }

            return IsBlocked;

        }

        private void BanPatient(Patient patient)
        {
            foreach (Patient p in PatientFileRepository.GetAll())
            {
                if (p.Username.Equals(patient.Username))
                {
                    SetInformationsAboutBanning(patient);
                }
            }
        }

        private void SetInformationsAboutBanning(Patient patient)
        {
            patient.Banned = true;
            patient.TimeOfBan = DateTime.Now;
            PatientFileRepository.UpdatePatient(patient.Username, patient);
        }

        public List<Therapy> GetTherapiesFromRecord(Patient patient)
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

        private MedicalRecord GetMedicalRecordForPatient(Patient patient)
        {
            MedicalRecord medicalRecord = new MedicalRecord();
            foreach (Patient p in PatientFileRepository.GetAll())
            {
                if (p.Username.Equals(patient.Username))
                {
                    medicalRecord = p.MedicalRecord;
                }
            }
            return medicalRecord;
        }
    }
}
