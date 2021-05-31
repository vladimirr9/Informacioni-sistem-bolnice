using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Doctor_ns;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    class DoctorControler
    {
        private DoctorService _doctorService = new DoctorService();

        public void Add(Doctor doctor)
        {
            _doctorService.Add(doctor);
        }

        public void Remove(Doctor doctor)
        {
            _doctorService.Remove(doctor);
        }

        public void Update(Doctor doctor)
        {
            _doctorService.Update(doctor);
        }

        public List<Doctor> GetAll()
        {
            return _doctorService.GetAll();
        }

        public Doctor GetOne(Doctor doctor)
        {
            return _doctorService.GetOne(doctor);
        }

        public List<Doctor> GetDoctorsByType(DoctorType type)
        {
            return _doctorService.GetDoctorsByType(type);
        }
        public List<Doctor> GetAvailableDoctorList(DateTime start, DateTime end)
        {
            return _doctorService.GetAvailableDoctorList(start, end);
        }
        public List<Doctor> GetFilteredDoctors(List<Doctor> doctors, DoctorType doctorType)
        {
            return _doctorService.GetFilteredDoctors(doctors, doctorType);
        }

        public void AddVacation(Doctor doctor, Vacation newVacation)
        {
            _doctorService.AddVacation(doctor, newVacation);
        }

        public void RemoveVacation(Doctor doctor, Vacation selectedVacation)
        {
            _doctorService.RemoveVacation(doctor, selectedVacation);
        }
    }
}
