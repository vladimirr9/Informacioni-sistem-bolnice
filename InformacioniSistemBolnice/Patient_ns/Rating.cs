using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice
{
    public class Rating
    {

        public int IdOfRating { get; set; }
        public String Comment { get; set; }
        public int Evaluation { get; set; }
        public String UsernameOfDoctor { get; set; }
        public String UsernameOfPatient { get; set; }
        public int IdOfAppointment { get; set; }
        public bool IsRatingDeleted { get; set; }

        public DateTime DateOfWritingRating { get; set; }
        
        [JsonIgnore]
        public Termin Appointment { get; set; }

        public Rating()
        {
        }

        public Rating(int idOfRating, string comment, int evaluation, string usernameOfDoctor, string usernameOfPatient, int idOfAppointment, bool isRatingDeleted, DateTime dateOfWritingRating)
        {
            IdOfRating = idOfRating;
            Comment = comment;
            Evaluation = evaluation;
            UsernameOfDoctor = usernameOfDoctor;
            UsernameOfPatient = usernameOfPatient;
            IdOfAppointment = idOfAppointment;
            IsRatingDeleted = isRatingDeleted;
            DateOfWritingRating = dateOfWritingRating;
           
        }
    }
}
