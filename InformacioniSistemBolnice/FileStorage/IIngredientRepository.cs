using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IIngredientRepository
    {
        List<Ingredient> GetAll();
        Ingredient GetOne(int id);
        Boolean Remove(int id);
        Boolean Add(Ingredient newIngredient);
        Boolean Update(int id, Ingredient newIngredient);
    }
}
