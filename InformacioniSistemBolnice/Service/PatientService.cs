using InformacioniSistemBolnice.Patient_ns.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class PatientService
    {
        private ActivityLogService _activityLogService = new ActivityLogService();
        private IAppointmentRepository _appointmentFileRepository = new AppointmentFileRepository();
        private IPatientRepository _patientRepository;

        public PatientService()
        {
            _patientRepository = new PatientFileRepository();
        }

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

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

            _patientRepository.Add(patient);
            return true;
        }

        public void Remove(Patient patient)
        {
            _patientRepository.Remove(patient.Username);
        }


        public void RemoveAllergen(Patient patient, Ingredient allergen)
        {
            patient.MedicalRecord.RemoveAlergen(allergen);
            _patientRepository.Update(patient.Username, patient);
        }

        public void AddAllergen(Patient patient, Ingredient allergen)
        {
            patient.MedicalRecord.AddAllergen(allergen);
            _patientRepository.Update(patient.Username, patient);
        }

        internal Patient GetOneByJMBG(string jmbg)
        {
            return _patientRepository.GetOneByJMBG(jmbg);
        }

        public void Update(string initialUsername, Patient patient)
        {
            Patient initialPatient = _patientRepository.GetOne(initialUsername);
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
            _patientRepository.Update(initialUsername, patient);


        }

        public List<Patient> GetAll()
        {
            List<Patient> patients = new List<Patient>();
            foreach (Patient patient in _patientRepository.GetAll())
            {
                if (!patient.IsDeleted)
                {
                    patients.Add(patient);
                }
                    
            }
            return patients;
        }

        public List<Patient> FilterPatients(List<Patient> patients, string filterVal, PatientFilter filter)
        {
            List<Patient> filteredPatients = new List<Patient>();
            foreach (Patient patient in patients)
            {
                if (filter.FitsFilter(patient, filterVal))
                {
                    filteredPatients.Add(patient);
                }
            }
            return filteredPatients;
        }

        public List<Patient> GetAvailablePatientList(DateTime start, DateTime end)
        {
            List<Patient> patients = new List<Patient>();
            foreach (Patient patient in _patientRepository.GetAll())
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
            return _patientRepository.GetOne(username);
        }


        public bool IsUsernameUnique(String username)
        {
            if (_patientRepository.GetOne(username) == null)
                return true;
            return false;
        }

        public bool IsJMBGUnique(String jmbg)
        {
            if (_patientRepository.GetOneByJMBG(jmbg) == null)
                return true;
            return false;
        }

        private void UpdateAppointmentsForUsernameChange(string username, string initialUsername)
        {
            foreach (Appointment appointment in _appointmentFileRepository.GetAll())
            {
                if (appointment.PatientUsername.Equals(initialUsername))
                {
                    appointment.PatientUsername = username;
                    _appointmentFileRepository.Update(appointment.AppointmentID, appointment);
                }
            }
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
            foreach (Patient p in _patientRepository.GetAll())
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
