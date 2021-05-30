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

        private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "ankete.json";
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

        public Rating GetOne(int idAnkete)
        {
            List<Rating> ankete = GetAll();
            foreach (Rating a in ankete)
            {
                if (a.IdOfRating.Equals(idAnkete))
                    return ankete[ankete.IndexOf(a)];
            }
            return null;
        }

        public static Boolean RemoveAnketa(int idAnketa)
        {
            List<Rating> ankete = GetAll();
            foreach (Rating a in ankete)
            {
                if (a.IdOfRating.Equals(idAnketa))
                {
                    ankete[ankete.IndexOf(a)].IsRatingDeleted = true;
                    Save(ankete);
                    return true;
                }
            }
            return false;
        }

        private static void Save(List<Rating> ankete)
        {
            string upis = JsonConvert.SerializeObject(ankete);
            File.WriteAllText(startupPath, upis);
        }

        public static Boolean AddAnketa(Rating novaRating)
        {
            List<Rating> ankete = GetAll();
            ankete.Add(novaRating);
            Save(ankete);
            return true;
        }

        public static Boolean UpdateAnketa(int idAnketa, Rating novaRating)
        {
            List<Rating> ankete = GetAll();
            foreach (Rating a in ankete)
            {
                if (a.IdOfRating.Equals(idAnketa))
                {
                    ankete[ankete.IndexOf(a)] = novaRating;
                    Save(ankete);
                    return true;
                }
            }
            return false;
        }

        public static Boolean Contains(int idTermina) {
            List<Rating> ankete = GetAll();
            foreach (Rating a in ankete) {
                if (a.IdOfAppointment.Equals(idTermina))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
