using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class NotesForAnamnesisController
    {
        private NotesForAnamnesisService _notesForAnamnesisService = new NotesForAnamnesisService();

        public List<Note> GetNotesWithReminder(Patient patient)
        {
            return _notesForAnamnesisService.NotesWithActiveReminder(patient);
        }

        public List<Note> NotesForAnamnesis(Anamnesis anamnesis, Patient patient)
        {
            return _notesForAnamnesisService.NotesForAnamnesis(anamnesis, patient);
        }

        public void AddNoteForAnamnesis(Note note, Anamnesis anamnesis)
        {
            _notesForAnamnesisService.AddNoteForAnamnesis(note, anamnesis);
        }

        public List<Anamnesis> GetPatientsAnamneses(Patient patient)
        {
            return _notesForAnamnesisService.PatientsAnamneses(patient);
        }
    }
}
