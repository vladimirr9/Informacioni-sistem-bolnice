using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice
{
    public class InformacijeOKoriscenjuFunkcionalnosti
    {

        public DateTime TrenutakIzvrsenjaFunkcionalnosti { get; set; }
        public string KorisnickoImePacijenta { get; set; }
        public VrstaFunkcionalnosti Vrsta { get; set; }
        

        

        public InformacijeOKoriscenjuFunkcionalnosti()
        {
        }

        public InformacijeOKoriscenjuFunkcionalnosti(DateTime trenutakIzvrsenjaFunkcionalnosti, string korisnickoImePacijenta, VrstaFunkcionalnosti vrsta)
        {
            TrenutakIzvrsenjaFunkcionalnosti = trenutakIzvrsenjaFunkcionalnosti;
            KorisnickoImePacijenta = korisnickoImePacijenta;
            Vrsta = vrsta;
        }
    }
}
