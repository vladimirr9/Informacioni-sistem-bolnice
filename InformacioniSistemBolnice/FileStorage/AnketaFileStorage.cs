using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public class AnketaFileStorage
    {

        private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "ankete.json";
        public static List<Anketa> GetAll()
        {
            if (!File.Exists(startupPath))
            {
                var tmp = File.OpenWrite(startupPath);
                tmp.Close();
            }
            List<Anketa> ankete;
            String procitano = File.ReadAllText(startupPath);
            if (procitano.Equals(""))
            {
                ankete = new List<Anketa>();
            }
            else
            {
                ankete = JsonConvert.DeserializeObject<List<Anketa>>(procitano);
            }
            return ankete;
        }

        public Anketa GetOne(int idAnkete)
        {
            List<Anketa> ankete = GetAll();
            foreach (Anketa a in ankete)
            {
                if (a.IdAnkete.Equals(idAnkete))
                    return ankete[ankete.IndexOf(a)];
            }
            return null;
        }

        public static Boolean RemoveAnketa(int idAnketa)
        {
            List<Anketa> ankete = GetAll();
            foreach (Anketa a in ankete)
            {
                if (a.IdAnkete.Equals(idAnketa))
                {
                    ankete[ankete.IndexOf(a)].IsDeleted = true;
                    Save(ankete);
                    return true;
                }
            }
            return false;
        }

        private static void Save(List<Anketa> ankete)
        {
            string upis = JsonConvert.SerializeObject(ankete);
            File.WriteAllText(startupPath, upis);
        }

        public static Boolean AddAnketa(Anketa novaAnketa)
        {
            List<Anketa> ankete = GetAll();
            ankete.Add(novaAnketa);
            Save(ankete);
            return true;
        }

        public static Boolean UpdateAnketa(int idAnketa, Anketa novaAnketa)
        {
            List<Anketa> ankete = GetAll();
            foreach (Anketa a in ankete)
            {
                if (a.IdAnkete.Equals(idAnketa))
                {
                    ankete[ankete.IndexOf(a)] = novaAnketa;
                    Save(ankete);
                    return true;
                }
            }
            return false;
        }

        public static Boolean Contains(int idTermina) {
            List<Anketa> ankete = GetAll();
            foreach (Anketa a in ankete) {
                if (a.IdTermina.Equals(idTermina))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
