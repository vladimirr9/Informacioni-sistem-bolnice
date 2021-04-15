using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice
{
    public class Alergen
    {
        public String naziv { get; set; }
        public Boolean isDeleted { get; set; }

        public Alergen(String naziv, Boolean isDeleted = false)
        {
            this.naziv = naziv;
            this.isDeleted = isDeleted;
        }

        public override string ToString()
        {
            return naziv;
        }

        public override bool Equals(object obj)
        {
            return obj is Alergen alergen &&
                   naziv == alergen.naziv;
        }
    }
}
