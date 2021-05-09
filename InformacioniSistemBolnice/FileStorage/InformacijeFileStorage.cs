using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public class InformacijeFileStorage
    {
        private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "informacije.json";

        public static List<InformacijeOKoriscenjuFunkcionalnosti> GetAll()
        {

            if (!File.Exists(startupPath))
            {
                var tmp = File.OpenWrite(startupPath);
                tmp.Close();
            }
            List<InformacijeOKoriscenjuFunkcionalnosti> informacije;
            String procitano = File.ReadAllText(startupPath);
            if (procitano.Equals(""))
            {
                informacije = new List<InformacijeOKoriscenjuFunkcionalnosti>();
            }
            else
            {
                informacije = JsonConvert.DeserializeObject<List<InformacijeOKoriscenjuFunkcionalnosti>>(procitano);
            }
            return informacije;


        }


        public static Boolean AddInformacije(InformacijeOKoriscenjuFunkcionalnosti novoKoriscenje)
        {
            List<InformacijeOKoriscenjuFunkcionalnosti> informacije = GetAll();
            informacije.Add(novoKoriscenje);
            Save(informacije);
            return true;
        }

        private static void Save(List<InformacijeOKoriscenjuFunkcionalnosti> informacije)
        {
            string upis = JsonConvert.SerializeObject(informacije);
            File.WriteAllText(startupPath, upis);
        }

        public static int kolikoJePutaIzvrsenaNekaFunkcionalnost(string korisnickoIme, VrstaFunkcionalnosti vrsta)
        {
            int brojacIzvrsenja = 0;
            List<InformacijeOKoriscenjuFunkcionalnosti> informacije = GetAll();
            foreach (InformacijeOKoriscenjuFunkcionalnosti i in informacije)
            {
                if (i.KorisnickoImePacijenta.Equals(korisnickoIme) && i.Vrsta.Equals(vrsta))
                {
                    if (i.IsDeleted == false)

                    {
                        ++brojacIzvrsenja;
                    }
                }

            }

            return brojacIzvrsenja;

        }

        public static void RemoveInformacijePacijenta(string korisnickoIme)
        {
            List<InformacijeOKoriscenjuFunkcionalnosti> informacije = GetAll();
            foreach (InformacijeOKoriscenjuFunkcionalnosti informacija in informacije)
            {
                if (informacija.KorisnickoImePacijenta.Equals(korisnickoIme))
                {
                    informacije[informacije.IndexOf(informacija)].IsDeleted = true;
                    Save(informacije);

                }
            }

        }

        




    }
}
