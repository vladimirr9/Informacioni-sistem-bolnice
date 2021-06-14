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

        public void UpdateWorkTime(Doctor doctor, string from, string to, DateTime? selectedDate)
        {
            DateTime start = GetStartOfWorkTime(from);
            DateTime end = GetEndOfWorkTime(to);
            if (selectedDate == null)
            {
                if (end <= start) 
                    return;
                else
                    UpdateRegularWorkHours(doctor, start, end);
            }
            else
            {
                if (end <= start)
                {
                    SetDateAsOffDay(doctor, selectedDate, start, end);
                    return;
                }
                else
                {
                    DateTime date = selectedDate.Value.Date;
                    if (doctor.WorkHours.AberrationExists(date))
                        UpdateExistingAberration(doctor, start, end, date);
                    else
                        AddNewAberrationToDoctor(doctor, start, end, date);
                }
            }
        }

        private static DateTime GetEndOfWorkTime(string to)
        {
            return DateTime.Parse(DateTime.Now.Date.ToString("dd/MM/yyyy") + " " + to);
        }

        private static DateTime GetStartOfWorkTime(string from)
        {
            return DateTime.Parse(DateTime.Now.Date.ToString("dd/MM/yyyy") + " " + from);
        }

        private static void SetDateAsOffDay(Doctor doctor, DateTime? selectedDate, DateTime start, DateTime end)
        {
            DateTime date = selectedDate.Value.Date;
            if (doctor.WorkHours.AberrationExists(date))
                UpdateExistingAberration(doctor, start.Date, end.Date, date);
            else
                AddNewAberrationToDoctor(doctor, start.Date, end.Date, date);
            return;
        }

        private static void UpdateRegularWorkHours(Doctor doctor, DateTime start, DateTime end)
        {
            doctor.WorkHours.Start = start;
            doctor.WorkHours.End = end;
        }

        private static void AddNewAberrationToDoctor(Doctor doctor, DateTime start, DateTime end, DateTime date)
        {
            var aberration = new WorkHourAberration(date, start, end);
            doctor.WorkHours.Aberrations.Add(aberration);
        }

        private static void UpdateExistingAberration(Doctor doctor, DateTime start, DateTime end, DateTime date)
        {
            var aberration = doctor.WorkHours.GetAberrationByDate(date);
            aberration.Start = start;
            aberration.End = end;
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
