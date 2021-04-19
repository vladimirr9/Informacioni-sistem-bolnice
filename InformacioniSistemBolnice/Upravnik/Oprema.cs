using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Oprema
    {
        private String sifra;
        private String naziv;
        private TipOpreme tipOpreme;
        private int kolicina;
        private Boolean isDeleted;
        private int idProstorije;

        public String Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        public String Sifra
        {
            get { return sifra; }
            set { sifra = value; }
        }

        public TipOpreme TipOpreme
        {
            get { return tipOpreme; }
            set { tipOpreme = value; }
        }
        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value; }
        }

        public Boolean IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public int IdProstorije
        {
            get { return idProstorije; }
            set { idProstorije = value; }
        }

        public Oprema() { }

        public Oprema(int idProstorije, String sifra, String naziv, TipOpreme tipOpreme, int kolicina, Boolean isDeleted)
        {
            IdProstorije = idProstorije;
            Sifra = sifra;
            Naziv = naziv;
            TipOpreme = tipOpreme;
            Kolicina = kolicina;
            IsDeleted = isDeleted;
        }

        /*public Oprema(string sifra, string naziv, TipOpreme tipOpreme, int kolicina, bool isDeleted)
        {
            this.sifra = sifra;
            this.naziv = naziv;
            this.tipOpreme = tipOpreme;
            this.kolicina = kolicina;
            this.isDeleted = isDeleted;
        }*/
    }
