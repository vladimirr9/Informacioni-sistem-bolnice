using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public class RatingFileRepository
    {

        private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "rating.json";
        public static List<Rating> GetAll()
        {
            if (!File.Exists(startupPath))
            {
                var tmp = File.OpenWrite(startupPath);
                tmp.Close();
            }
            List<Rating> ankete;
            String procitano = File.ReadAllText(startupPath);
            if (procitano.Equals(""))
            {
                ankete = new List<Rating>();
            }
            else
            {
                ankete = JsonConvert.DeserializeObject<List<Rating>>(procitano);
            }
            return ankete;
        }

        public static Rating GetOne(int RatingID)
        {
            List<Rating> ankete = GetAll();
            foreach (Rating a in ankete)
            {
                if (a.IdOfRating.Equals(RatingID))
                    return ankete[ankete.IndexOf(a)];
            }
            return null;
        }

        public static Boolean RemoveRating(int RatingID)
        {
            List<Rating> ankete = GetAll();
            foreach (Rating a in ankete)
            {
                if (a.IdOfRating.Equals(RatingID))
                {
                    ankete[ankete.IndexOf(a)].IsRatingDeleted = true;
                    Save(ankete);
                    return true;
                }
            }
            return false;
        }

        private static void Save(List<Rating> ratings)
        {
            string upis = JsonConvert.SerializeObject(ratings);
            File.WriteAllText(startupPath, upis);
        }

        public static Boolean AddRating(Rating novaRating)
        {
            List<Rating> ankete = GetAll();
            ankete.Add(novaRating);
            Save(ankete);
            return true;
        }

        public static Boolean UpdateRating(int idOfRating, Rating newRating)
        {
            List<Rating> ankete = GetAll();
            foreach (Rating a in ankete)
            {
                if (a.IdOfRating.Equals(idOfRating))
                {
                    ankete[ankete.IndexOf(a)] = newRating;
                    Save(ankete);
                    return true;
                }
            }
            return false;
        }

        
    }
}
