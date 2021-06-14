using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class BanningPatientService
    {
        private ActivityLogService _activityLogService = new ActivityLogService();
        private IPatientRepository _patientRepository = new PatientFileRepository();

        public Boolean CheckStatusOfPatient(Patient patient)
        {
            Boolean IsBanned = false;
            int numberOfMakingAppointment = _activityLogService.NumberOfActivity(patient.Username, TypeOfActivity.makingAppointment);
            int numberOfEditingAppointment = _activityLogService.NumberOfActivity(patient.Username, TypeOfActivity.editingAppointment);
            int numberOfCancelingAppointment = _activityLogService.NumberOfActivity(patient.Username, TypeOfActivity.cancellingAppointment);

            if (numberOfMakingAppointment > 5 || numberOfCancelingAppointment > 5 || numberOfEditingAppointment > 5)
            {
                BanPatient(patient);
                IsBanned = true;
            }

            return IsBanned;

        }

        private void BanPatient(Patient patient)
        {
            foreach (Patient p in _patientRepository.GetAll())
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
            _patientRepository.Update(patient.Username, patient);
        }

    }
}
