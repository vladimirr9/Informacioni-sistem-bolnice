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

        public Obavestenje(string naslov, string tekst, DateTime datumNastanka, bool procitano = false, bool isDeleted = false)
        {
            idObavestenja = ObavestenjeFileStorage.GetAll().Count();
            this.naslov = naslov;
            this.tekst = tekst;
            this.datumNastanka = datumNastanka;
            this.procitano = procitano;
            this.isDeleted = isDeleted;
        }
    }
}
