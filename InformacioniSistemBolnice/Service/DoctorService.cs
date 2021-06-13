using InformacioniSistemBolnice.Doctor_ns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    class DoctorService
    {
        private IDoctorRepository _doctorRepository;

        public DoctorService()
        {
            _doctorRepository = new DoctorFileRepository();
        }

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public void Add(Doctor doctor)
        {
            _doctorRepository.Add(doctor);
        }

        public void Remove(Doctor doctor)
        {
            _doctorRepository.Remove(doctor.Username);
        }

        public void Update(Doctor doctor)
        {
            _doctorRepository.Update(doctor.Username, doctor);
        }

        public List<Doctor> GetAll()
        {
            return _doctorRepository.GetAll();
        }

        public Doctor GetOne(Doctor doctor)
        {
            return _doctorRepository.GetOne(doctor.Username);
        }

        public List<Doctor> GetDoctorsByType(DoctorType type)
        {
            List<Doctor> doctors = new List<Doctor>();
            foreach (Doctor doctor in _doctorRepository.GetAll())
            {
                if (doctor.doctorType.Equals(type))
                {
                    doctors.Add(doctor);
                }
            }
            return doctors;
        }
        public List<Doctor> GetAvailableDoctorList(DateTime start, DateTime end)
        {
            List<Doctor> doctors = new List<Doctor>();
            foreach (Doctor doctor in _doctorRepository.GetAll())
            {
                if (doctor.IsAvailable(start, end) && !doctor.IsDeleted)
                {
                    doctors.Add(doctor);
                }
            }
            return doctors;
        }

        internal void RemoveVacation(Doctor doctor, Vacation selectedVacation)
        {
            doctor.Vacations.Remove(selectedVacation);
            doctor.DaysOfVacation += selectedVacation.DurationInBusinessDays;
        }

        public void AddVacation(Doctor doctor, Vacation newVacation)
        {
            doctor.DaysOfVacation -= newVacation.DurationInBusinessDays;
            doctor.Vacations.Add(newVacation);
        }

        public List<Doctor> GetFilteredDoctors(List<Doctor> doctors, DoctorType doctorType)
        {
            List<Doctor> filteredDoctors = new List<Doctor>();
            foreach (Doctor doctor in doctors)
            {
                if (doctor.doctorType == doctorType)
                    filteredDoctors.Add(doctor);
            }
            return filteredDoctors;
        }

        public String GetType(Doctor doctor)
        {
            string type = "";
            switch (doctor.doctorType)
            {
                case DoctorType.generalPractitioner:
                    type = "Opšta praksa";
                    break;
                case DoctorType.cardiologist:
                    type = "Kardiolog";
                    break;
                case DoctorType.neurologist:
                    type = "Neurolog";
                    break;
                case DoctorType.pediatrician:
                    type = "Pedijatar";
                    break;
                case DoctorType.surgeon:
                    type = "Hirurg";
                    break;
            }

            return type;
        }
    }
}
