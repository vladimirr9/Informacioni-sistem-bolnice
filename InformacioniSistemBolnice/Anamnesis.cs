using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice
{
    public class Anamnesis
    {
        public String DescriptionOfAnamnesis { get; set; }
        public List<Note> NotesForAnamnesis { get; set; }
        public String UsernameOfPatient { get; set; }
        public int IdOfAnamnesis { get; set; }
        public DateTime DateOfWritingAnamnesis { get; set; }
        public int IdOfAppointment { get; set; }

        public Anamnesis()
        {
        }

        public Anamnesis(string descriptionOfAnamnesis, List<Note> notesForAnamnesis, string usernameOfPatient, int idOfAnamnesis, DateTime dateOfWritingAnamnesis, int idOfAppintment)
        {
            DescriptionOfAnamnesis = descriptionOfAnamnesis;
            NotesForAnamnesis = notesForAnamnesis;
            UsernameOfPatient = usernameOfPatient;
            IdOfAnamnesis = idOfAnamnesis;
            DateOfWritingAnamnesis = dateOfWritingAnamnesis;
            IdOfAppointment = idOfAppintment;
        }
    }
}
