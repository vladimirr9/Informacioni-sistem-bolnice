using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Patient_ns;
using InformacioniSistemBolnice.Secretary_ns;
using InformacioniSistemBolnice.Secretary_ns.HelpWizard;
using InformacioniSistemBolnice.Upravnik;

namespace InformacioniSistemBolnice.Service
{
    class LoginService
    {

        public Patient FindPatient(String username, String password)
        {
            List<Patient> patients = PatientFileRepository.GetAll();
            foreach (var patient in patients)
            {
                if (username.Equals(patient.Username) && password.Equals(patient.Password))
                {
                    return patient;
                }
            }

            return null;
        }

        public Secretary FindSecretary(String username, String password)
        {
            List<Secretary> secretaries = SecretaryFileRepository.GetAll();
            foreach (Secretary secretary in secretaries)
            {
                if (username.Equals(secretary.Username) && password.Equals(secretary.Password))
                {
                    return secretary;
                }
            }

            return null;
        }

        public Doctor FindDoctor(String username, String password)
        {
            List<Doctor> doctors = DoctorFileRepository.GetAll();
            foreach (Doctor doctor in doctors)
            {
                if (username.Equals(doctor.Username) && password.Equals(doctor.Password))
                {
                    return doctor;
                }
            }

            return null;
        }
        public Manager FindManager(String username, String password)
        {
            List<Manager> upravnikLista = ManagerFileRepository.GetAll();
            foreach (Manager u in upravnikLista)
            {
                if (username.Equals(u.Username) && password.Equals(u.Password))
                {
                    return u;
                }
            }

            return null;
        }
    }
}
