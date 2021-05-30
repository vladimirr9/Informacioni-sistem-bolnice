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

        public List<Note> GetNotesWithReminder(Pacijent patient)
        {
            return _anamnesisService.NotesWithActiveReminder(patient);
        }

        public List<Note> NotesForAnamnesis(Anamnesis anamnesis,Pacijent patient)
        {
            return _anamnesisService.NotesForAnamnesis(anamnesis,patient);
        }

        public void AddNoteForAnamnesis(Note note, Anamnesis anamnesis)
        {
            _anamnesisService.AddNoteForAnamnesis(note,anamnesis);
        }
    }
}
