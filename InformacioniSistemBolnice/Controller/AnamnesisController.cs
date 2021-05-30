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

        public List<Note> NotesForAnamnesis(Anamnesis anamnesis, Patient patient)
        {
            return _anamnesisService.NotesForAnamnesis(anamnesis, patient);
        }

        public void AddNoteForAnamnesis(Note note, Anamnesis anamnesis)
        {
            _anamnesisService.AddNoteForAnamnesis(note, anamnesis);
        }

        public void UpdateAnamnesis(int idOfAnamnesis, Anamnesis anamnesis)
        {
            _anamnesisService.UpdateAnamnesis(idOfAnamnesis,anamnesis);
        }
    }
}
