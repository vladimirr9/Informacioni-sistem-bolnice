using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice
{
    public class Anketa
    {

        public int IdAnkete { get; set; }
        public String Komentar { get; set; }
        public int Ocena { get; set; }
        public String KorisnickoImeLekara { get; set; }
        public String KorisnickoImePacijenta { get; set; }
        public int IdTermina { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime NastanakAnkete { get; set; }
        
        [JsonIgnore]
        public Termin Termin { get; set; }

        public Anketa()
        {
        }

        public Anketa(int idAnkete, string komentar, int ocena, string korisnickoImeLekara, string korisnickoImePacijenta, int idTermina, bool isDeleted, DateTime nastanakAnkete)
        {
            IdAnkete = idAnkete;
            Komentar = komentar;
            Ocena = ocena;
            KorisnickoImeLekara = korisnickoImeLekara;
            KorisnickoImePacijenta = korisnickoImePacijenta;
            IdTermina = idTermina;
            IsDeleted = isDeleted;
            NastanakAnkete = nastanakAnkete;
           
        }
    }
}
