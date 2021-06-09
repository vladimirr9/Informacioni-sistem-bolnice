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

        public bool Register(Patient patient)
        {
            if (!IsUsernameUnique(patient.Username))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni",
                    MessageBoxButton.OK);
                return false;
            }

            if (!IsJMBGUnique(patient.JMBG))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return false;
            }
            
            PatientFileRepository.AddPatient(patient);
            return true;
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

        internal Patient GetOneByJMBG(string jmbg)
        {
            return PatientFileRepository.GetOneByJMBG(jmbg);
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

        public List<Patient> GetAll()
        {
            List<Patient> patients = new List<Patient>();
            foreach (Patient patient in PatientFileRepository.GetAll())
            {
                if (!patient.IsDeleted)
                {
                    patients.Add(patient);
                }
                    
            }
            return patients;
        }

        public List<Patient> GetAvailablePatientList(DateTime start, DateTime end)
        {
            List<Patient> patients = new List<Patient>();
            foreach (Patient patient in PatientFileRepository.GetAll())
            {
                if (patient.IsAvailable(start, end) && !patient.IsDeleted)
                {
                    patients.Add(patient);
                }
            }
            return patients;
        }

        public Patient GetOne(String username)
        {
            return PatientFileRepository.GetOne(username);
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
            Boolean IsBanned = false;
            int numberOfMakingAppointment = _activityLogService.NumberOfActivity(patient.Username, TypeOfActivity.makingAppointment);
            int numberOfEditingAppointment = _activityLogService.NumberOfActivity(patient.Username, TypeOfActivity.editingAppointment);
            int numberOfCancelingAppointment = _activityLogService.NumberOfActivity(patient.Username, TypeOfActivity.cancellingAppointment);

            if (numberOfMakingAppointment > 3 || numberOfCancelingAppointment > 2 || numberOfEditingAppointment > 2)
            {
                BanPatient(patient);
                IsBanned = true;
            }

            return IsBanned;

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

        public List<Prescription> GetPrescriptionsForPatient(Patient patient)
        {
            List<Prescription> prescriptions = new List<Prescription>();
            foreach (Prescription p in GetMedicalRecordForPatient(patient).recept)
            {
                prescriptions.Add(p);
            }

            return prescriptions;
        }

        public Boolean IsValidPassword(Patient patient, String password)
        {
            Boolean temp = false;
            foreach (Patient p in GetAll())
            {
                if (p.Username.Equals(patient.Username) && p.Password.Equals(password))
                {
                    temp = true;
                    break;
                }
            }

            return temp;
        }

        public void SetNewPassword(Patient patient, String newPassword)
        {
            foreach (Patient p in GetAll())
            {
                if (p.Username.Equals(patient.Username))
                {
                    p.Password = newPassword;
                    Update(p.Username, p);
                    break;
                }

            }

        }
    }
}
