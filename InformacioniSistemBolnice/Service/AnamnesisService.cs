using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class AnamnesisService
    {
        private IAnamnesisRepository _anamnesisFileRepository = new AnamnesisFileRepository();
        public void UpdateAnamnesis(int AnamnesisID, Anamnesis anamnesis)
        {
            _anamnesisFileRepository.Update(AnamnesisID, anamnesis);
        }

        public Anamnesis AppointmentAnamnesis(Appointment appointment)
        {
            foreach (Anamnesis anamnesis in _anamnesisFileRepository.GetAll())
            {
                if (anamnesis.IdOfAppointment.Equals(appointment.AppointmentID))
                {
                    return anamnesis;
                }
            }
            return null;

        }

        public int GenerateId()
        {
            return _anamnesisFileRepository.GetAll().Count + 1;
        }

        public void Add(Anamnesis anamnesis)
        {
            _anamnesisFileRepository.Add(anamnesis);
        }

        public void Update(Anamnesis anamnesis, Appointment appointment)
        {
            if (AppointmentAnamnesis(appointment) != null)
            {
                _anamnesisFileRepository.Update(anamnesis.IdOfAnamnesis, anamnesis);
            }
            else
            {
                _anamnesisFileRepository.Add(anamnesis);
            }
        }

    }

}

