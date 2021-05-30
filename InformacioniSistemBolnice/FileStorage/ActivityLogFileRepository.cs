using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public class ActivityLogFileRepository
    {
        private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "informacije.json";

        public static List<ActivityLog> GetAll()
        {

            if (!File.Exists(startupPath))
            {
                var tmp = File.OpenWrite(startupPath);
                tmp.Close();
            }
            List<ActivityLog> informacije;
            String procitano = File.ReadAllText(startupPath);
            if (procitano.Equals(""))
            {
                informacije = new List<ActivityLog>();
            }
            else
            {
                informacije = JsonConvert.DeserializeObject<List<ActivityLog>>(procitano);
            }
            return informacije;


        }


        public static Boolean AddInformacije(ActivityLog novoKoriscenje)
        {
            List<ActivityLog> informacije = GetAll();
            informacije.Add(novoKoriscenje);
            Save(informacije);
            return true;
        }

        private static void Save(List<ActivityLog> informacije)
        {
            string upis = JsonConvert.SerializeObject(informacije);
            File.WriteAllText(startupPath, upis);
        }

        public static int BrojIzvrsenihFunkcionalnosti(string korisnickoIme, TypeOfActivity vrsta)
        {
            int brojacIzvrsenja = 0;
            List<ActivityLog> informacije = GetAll();
            foreach (ActivityLog i in informacije)
            {
                if (i.UsernameOfPatient.Equals(korisnickoIme) && i.Type.Equals(vrsta))
                {
                    if (i.DateOfActivity> DateTime.Now.AddDays(-10))

                    {
                        ++brojacIzvrsenja;
                    }
                }

            }

            return brojacIzvrsenja;

        }

        

        




    }
}
