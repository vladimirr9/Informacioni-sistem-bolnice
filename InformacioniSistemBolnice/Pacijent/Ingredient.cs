using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice
{
    public class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public Ingredient(int id, string name, bool isDeleted = false)
        {
            this.ID = id;
            this.Name = name;
            this.IsDeleted = isDeleted;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Ingredient ingredient &&
                   ID == ingredient.ID;
        }
    }
}
