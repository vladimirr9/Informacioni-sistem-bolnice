using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class RatingService
    {
        private IRatingRepository _ratingRepository = new RatingFileRepository();
        public List<Rating> GetAll()
        {
            return _ratingRepository.GetAll();
        }

        public Rating GetOne(int idOfRating)
        {
            return _ratingRepository.GetOne(idOfRating);
        }

        public Boolean Remove(int idOfRating)
        {
            return _ratingRepository.Remove(idOfRating);
        }

        public Boolean Add(Rating newRating)
        {
            return _ratingRepository.Add(newRating);
        }

        public Boolean Update(int idOfRating, Rating newRating)
        {
            return _ratingRepository.Update(idOfRating, newRating);
        }

        public  Boolean Contains(int idOfAppointment)
        {
            foreach (Rating a in _ratingRepository.GetAll())
            {
                if (a.IdOfAppointment.Equals(idOfAppointment))
                {
                    return true;
                }
            }
            return false;
        }

        

    }
}
