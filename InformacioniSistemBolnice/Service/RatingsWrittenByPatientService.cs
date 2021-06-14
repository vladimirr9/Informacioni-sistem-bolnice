using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    public class RatingsWrittenByPatientService
    {
        private RatingService _ratingService = new RatingService();
        private List<Rating> GetRatingsWrittenByPatient(Patient patient)
        {
            List<Rating> ratings = new List<Rating>();

            foreach (Rating a in _ratingService.GetAll())
            {
                if (a.UsernameOfDoctor == null && a.IdOfAppointment == 0)
                {
                    if (a.UsernameOfPatient.Equals(patient.Username))
                    {
                        ratings.Add(a);
                    }
                }
            }
            return ratings;
        }

        private Rating GetLastWrittenRatingByPatient(Patient patient)
        {
            Rating rating = new Rating();
            DateTime lastWritten = DateTime.Parse("1970-01-01" + " " + "00:00:00");
            if (GetRatingsWrittenByPatient(patient).Count != 0)
            {
                lastWritten = GetRatingsWrittenByPatient(patient).ElementAt(0).DateOfWritingRating;
            }

            foreach (Rating a in GetRatingsWrittenByPatient(patient))
            {
                if (lastWritten < a.DateOfWritingRating)
                {
                    lastWritten = a.DateOfWritingRating;
                    rating = a;

                }
            }

            return rating;
        }

        public Boolean IsCheckedCondition(Patient patient)
        {
            if (GetRatingsWrittenByPatient(patient).Count == 0 || DateOfLastWrittenRating(patient).AddSeconds(15) < DateTime.Now)
            {
                return true;
            }

            return false;
        }

        private DateTime DateOfLastWrittenRating(Patient patient)
        {
            return GetLastWrittenRatingByPatient(patient).DateOfWritingRating;
        }

        public int GetNumberOfRating(int number, Doctor doctor)
        {
            int counter = 0;
            foreach (Rating r in _ratingService.GetAll())
            {
                if (r.UsernameOfDoctor.Equals(doctor.Username) && r.Evaluation == number)
                {
                    counter += 1;
                }
            }

            return counter;
        }
    }
}
