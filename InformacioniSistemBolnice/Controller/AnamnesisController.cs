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

        public List<Note> GetNotesWithReminder(Patient patient)
        {
            return _anamnesisService.NotesWithActiveReminder(patient);
        }
    }
}
