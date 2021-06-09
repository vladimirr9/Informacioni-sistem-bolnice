using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class RatingController
    {
        private RatingService _ratingService = new RatingService();

        public List<Rating> GetAll()
        {
            return _ratingService.GetAll();
        }

        public Rating GetOne(Rating rating)
        {
            return _ratingService.GetOne(rating.IdOfRating);
        }

        public Boolean Remove(Rating rating)
        {
            return _ratingService.Remove(rating.IdOfRating);
        }

        public Boolean Add(Rating newRating)
        {
            return _ratingService.Add(newRating);
        }

        public Boolean Update(int idOfRating, Rating newRating)
        {
            return _ratingService.Update(idOfRating, newRating);
        }

        public Boolean Contains(int idOfAppointment)
        {
            return _ratingService.Contains(idOfAppointment);
        }

        public Boolean IsCheckedCondition(Patient patient)
        {
            return _ratingService.IsCheckedCondition(patient);
        }

        public int GetNumberOfRating(int number, Doctor doctor)
        {
            return _ratingService.GetNumberOfRating(number, doctor);
        }
    }
}
