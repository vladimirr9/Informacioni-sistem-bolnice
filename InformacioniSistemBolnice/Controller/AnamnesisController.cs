using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class AnamnesisController
    {
        private AnamnesisService _anamnesisService = new AnamnesisService();

       

        public void UpdateAnamnesis(int idOfAnamnesis, Anamnesis anamnesis)
        {
            _anamnesisService.UpdateAnamnesis(idOfAnamnesis, anamnesis);
        }
        public Anamnesis AppointmentAnamnesis(Appointment appointment)
        {
            return _anamnesisService.AppointmentAnamnesis(appointment);

        }

        public int GenerateId()
        {
            return _anamnesisService.GenerateId();
        }

        public void Add(Anamnesis anamnesis)
        {
            _anamnesisService.Add(anamnesis);
        }

        public void Update(Anamnesis anamnesis, Appointment appointment)
        { 
            _anamnesisService.Update(anamnesis, appointment);

        }
    }
}
