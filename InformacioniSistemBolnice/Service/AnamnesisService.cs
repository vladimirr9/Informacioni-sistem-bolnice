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
        private List<Anamnesis> PatientsAnamneses(Pacijent patient)
        {
            List<Anamnesis> anamneses = new List<Anamnesis>();
            foreach (Anamnesis a in AnamnesisFileRepository.GetAll())
            {
                if (a.UsernameOfPatient.Equals(patient.korisnickoIme))
                {
                    anamneses.Add(a);
                }
            }
            return anamneses;
        }

        private List<Note> NotesWithReminder(Pacijent patient)
        {
            List<Note> notes = new List<Note>();
            foreach (Anamnesis a in PatientsAnamneses(patient))
            {
                foreach (Note n in a.NotesForAnamnesis)
                {
                    if (n.IsSetReminder == true)
                    {
                        notes.Add(n);
                    }
                }
            }
            return notes;
        }

        public List<Note> NotesWithActiveReminder(Pacijent patient)
        {
            List<Note> notes = new List<Note>();
            foreach (Note n in NotesWithReminder(patient))
            {
                if (n.StartDate < DateTime.Now && n.EndDate > DateTime.Now)
                {
                    notes.Add(n);
                }
            }
            return notes;
        }



    }

}

