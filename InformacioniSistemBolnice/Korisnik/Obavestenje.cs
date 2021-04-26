using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Korisnik
{

    public class Obavestenje
    {
        public int idObavestenja { get; set; }
        public String naslov { get; set; }
        public String tekst { get; set; }
        public DateTime datumNastanka { get; set; }
        public Boolean procitano { get; set; }
        public Boolean isDeleted { get; set; }
        public String korisnickoIme { get; set; }

        public Obavestenje(int idObavestenja, string naslov, string tekst, DateTime datumNastanka, String korisnickoIme = null, bool procitano = false, bool isDeleted = false)
        {
            this.idObavestenja = idObavestenja;
            this.korisnickoIme = korisnickoIme;
            this.naslov = naslov;
            this.tekst = tekst;
            this.datumNastanka = datumNastanka;
            this.procitano = procitano;
            this.isDeleted = isDeleted;
        }

        public String Header
        {
            get
            {
                if (korisnickoIme == null)
                    return datumNastanka.ToString("dd/MM/yyyy HH:mm  - ") + naslov + " GLOBAL";
                else
                    return datumNastanka.ToString("dd/MM/yyyy HH:mm  - ") + naslov;
            }   
        }
    }
}
