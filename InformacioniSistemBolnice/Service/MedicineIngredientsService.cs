using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class MedicineIngredientsService
    {

        private IIngredientRepository _ingredientFileRepository = new IngredientFileRepository();
        public List<Ingredient> GetMedicineIngredients(Medicine medicine)
        {
            List<Ingredient> ingredientsList = new List<Ingredient>();
            foreach (Ingredient ingredient in _ingredientFileRepository.GetAll())
            {
                if (medicine.IngredientsList.Contains(ingredient))
                {
                    ingredientsList.Add(ingredient);
                }
            }
            return ingredientsList;
        }

        public ObservableCollection<Ingredient> AddIngredients(Medicine medicine, Ingredient ingredient)
        {
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>(medicine.IngredientsList);
            foreach (Ingredient ing in GetAllIngredients())
            {
                if (!ingredients.Contains(ing) && ingredient.Name.Equals(ing.Name))
                {
                    ingredients.Add(ing);
                }
            }
            return ingredients;
        }

        public void AddIngredientsToNewMedicine(ItemCollection items, Ingredient ingredient)
        {
            foreach (Ingredient ing in GetAllIngredients())
            {
                if (!items.Contains(ingredient) && ing.Name.Equals(ingredient.Name))
                {
                    items.Add(ing);
                }
            }
        }

        public ObservableCollection<Ingredient> RemoveIngredient(Medicine medicine, Ingredient ingredient)
        {
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>(medicine.IngredientsList);
            foreach (Ingredient ing in GetAllIngredients())
            {
                if (ing.Name.Equals(ingredient.Name))
                {
                    ingredients.Remove(ing);
                }
            }
            return ingredients;
        }
        public ObservableCollection<Ingredient> RemoveIngredientFromNewMedicine(Ingredient ingredient)
        {
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
            foreach (Ingredient ing in GetAllIngredients())
            {
                if (ing.Name.Equals(ingredient.Name))
                {
                    ingredients.Remove(ing);
                }
            }
            return ingredients;
        }

        public List<Ingredient> GetAllIngredients()
        {
            return _ingredientFileRepository.GetAll();
        }

    }
}
