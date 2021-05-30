using System;
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
    }
}
