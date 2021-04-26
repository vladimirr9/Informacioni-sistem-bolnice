using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice
{
    public class Sastojak
    {
        public int id { get; set; }
        public String naziv { get; set; }
        public Boolean isDeleted { get; set; }

        public Sastojak(int id, String naziv, Boolean isDeleted = false)
        {
            this.id = id;
            this.naziv = naziv;
            this.isDeleted = isDeleted;
        }

        public override string ToString()
        {
            return naziv;
        }

        public override bool Equals(object obj)
        {
            return obj is Sastojak sastojak &&
                   id == sastojak.id;
        }
    }
}
