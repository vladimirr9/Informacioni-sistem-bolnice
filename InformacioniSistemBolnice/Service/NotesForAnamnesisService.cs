﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class NotesForAnamnesisService
    {
        private IAnamnesisRepository _anamnesisFileRepository = new AnamnesisFileRepository();
        public List<Anamnesis> PatientsAnamneses(Patient patient)
        {
            List<Anamnesis> anamneses = new List<Anamnesis>();
            foreach (Anamnesis a in _anamnesisFileRepository.GetAll())
            {
                if (a.UsernameOfPatient.Equals(patient.Username))
                {
                    anamneses.Add(a);
                }
            }
            return anamneses;
        }

        private List<Note> NotesWithReminder(Patient patient)
        {
            List<Note> notes = new List<Note>();
            if (PatientsAnamneses(patient).Count() > 0)
            {
                foreach (Anamnesis a in PatientsAnamneses(patient))
                {
                    if (a.NotesForAnamnesis != null)
                    {

                        foreach (Note n in a.NotesForAnamnesis)
                        {
                            if (n.IsSetReminder == true)
                            {
                                notes.Add(n);
                            }
                        }
                    }

                }
            }

            return notes;
        }

        public List<Note> NotesWithActiveReminder(Patient patient)
        {
            List<Note> notes = new List<Note>();
            foreach (Note n in NotesWithReminder(patient))
            {
                if (n.StartPeriodOfTime.TimeOfDay < DateTime.Now.TimeOfDay &&
                    n.EndPeriodOfTime.TimeOfDay > DateTime.Now.TimeOfDay)
                {
                    notes.Add(n);
                }
            }
            return notes;
        }

        public List<Note> NotesForAnamnesis(Anamnesis anamnesis, Patient patient)
        {
            List<Note> notes = new List<Note>();
            foreach (Anamnesis a in PatientsAnamneses(patient))
            {
                if (a.IdOfAnamnesis.Equals(anamnesis.IdOfAnamnesis))
                {
                    if (a.NotesForAnamnesis != null)
                    {
                        foreach (Note n in a.NotesForAnamnesis)
                        {
                            notes.Add(n);
                        }
                    }
                }
            }
            return notes;
        }
        public void AddNoteForAnamnesis(Note note, Anamnesis anamnesis)
        {
            anamnesis.NotesForAnamnesis.Add(note);
            _anamnesisFileRepository.Update(anamnesis.IdOfAnamnesis, anamnesis);
        }

    }
}
