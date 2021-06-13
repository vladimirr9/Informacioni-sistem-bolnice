using InformacioniSistemBolnice.User;
using InformacioniSistemBolnice.Doctor_ns;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace InformacioniSistemBolnice.FileStorage
{
    public class IngredientFileRepository : IIngredientRepository
    {
        private string _startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "ingredients.json";

        public List<Ingredient> GetAll()
        {
            if (!File.Exists(_startupPath))
            {
                var tmp = File.OpenWrite(_startupPath);
                tmp.Close();
            }
            List<Ingredient> ingredients;
            String allText = File.ReadAllText(_startupPath);
            if (allText.Equals(""))
            {
                ingredients = new List<Ingredient>();
            }
            else
            {
                ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(allText);
            }
            return ingredients;
        }

        public Ingredient GetOne(int id)
        {
            List<Ingredient> ingredients = GetAll();
            foreach (Ingredient ingredient in ingredients)
            {
                if (ingredient.ID.Equals(id))
                    return ingredients[ingredients.IndexOf(ingredient)];
            }
            return null;
        }

        public Boolean Remove(int id)
        {
            List<Ingredient> ingredients = GetAll();
            foreach (Ingredient ingredient in ingredients)
            {
                if (ingredient.ID.Equals(id))
                {
                    ingredients[ingredients.IndexOf(ingredient)].IsDeleted = true;
                    Save(ingredients);
                    return true;
                }
            }
            return false;
        }

        public Boolean Add(Ingredient newIngredient)
        {
            List<Ingredient> ingredients = GetAll();
            ingredients.Add(newIngredient);
            Save(ingredients);
            return true;
        }

        public Boolean Update(int id, Ingredient newIngredient)
        {
            List<Ingredient> ingredients = GetAll();
            foreach (Ingredient ingredient in ingredients)
            {
                if (ingredient.ID.Equals(id))
                {
                    ingredients[ingredients.IndexOf(ingredient)] = newIngredient;
                    Save(ingredients);
                    return true;
                }
            }
            return false;
        }

        private void Save(List<Ingredient> ingredients)
        {
            string contents = JsonConvert.SerializeObject(ingredients);
            File.WriteAllText(_startupPath, contents);
        }

    }
}