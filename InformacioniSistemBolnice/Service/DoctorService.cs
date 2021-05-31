﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    class DoctorService
    {
        public void Add(Doctor doctor)
        {
            DoctorFileRepository.AddDoctor(doctor);
        }

        public void Remove(Doctor doctor)
        {
            DoctorFileRepository.RemoveDoctor(doctor.Username);
        }

        public void Update(Doctor doctor)
        {
            DoctorFileRepository.UpdateDoctor(doctor.Username, doctor);
        }

        public List<Doctor> GetAll()
        {
            return DoctorFileRepository.GetAll();
        }

        public Doctor GetOne(Doctor doctor)
        {
            return DoctorFileRepository.GetOne(doctor.Username);
        }

        public List<Doctor> GetDoctorsByType(DoctorType type)
        {
            List<Doctor> doctors = new List<Doctor>();
            foreach (Doctor doctor in DoctorFileRepository.GetAll())
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
            List<Doctor> doctors = new List<global::Doctor>();
            foreach (global::Doctor doctor in DoctorFileRepository.GetAll())
            {
                if (doctor.IsAvailable(start, end) && !doctor.IsDeleted)
                {
                    doctors.Add(doctor);
                }
            }
            return doctors;
        }
        public List<Doctor> GetFilteredDoctors(List<global::Doctor> doctors, DoctorType doctorType)
        {
            List<Doctor> filteredDoctors = new List<global::Doctor>();
            foreach (Doctor doctor in doctors)
            {
                if (doctor.doctorType == doctorType)
                    filteredDoctors.Add(doctor);
            }
            return filteredDoctors;
        }
    }
}
