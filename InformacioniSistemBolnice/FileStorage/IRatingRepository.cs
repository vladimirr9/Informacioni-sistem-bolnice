using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IRatingRepository
    {
        List<Rating> GetAll();

        Rating GetOne(int RatingID);
        Boolean Remove(int RatingID);

        Boolean Add(Rating novaRating);

        Boolean Update(int idOfRating, Rating newRating);
    }
}
