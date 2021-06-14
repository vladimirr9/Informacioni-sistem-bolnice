using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class MedicineIngredientsController
    {
        private MedicineIngredientsService _medicineIngredientsService = new MedicineIngredientsService();
        public List<Ingredient> GetMedicineIngredients(Medicine medicine)
        {
            return _medicineIngredientsService.GetMedicineIngredients(medicine);
        }
        public ObservableCollection<Ingredient> AddIngredients(Medicine medicine, Ingredient ingredient)
        {
            return _medicineIngredientsService.AddIngredients(medicine, ingredient);
        }
        public ObservableCollection<Ingredient> RemoveIngredient(Medicine medicine, Ingredient ingredient)
        {
            return _medicineIngredientsService.RemoveIngredient(medicine, ingredient);
        }
        public ObservableCollection<Ingredient> RemoveIngredientFromNewMedicine(Ingredient ingredient)
        {
            return _medicineIngredientsService.RemoveIngredientFromNewMedicine(ingredient);
        }

        public void AddIngredientsToNewMedicine(ItemCollection items, Ingredient ingredient)
        {
            _medicineIngredientsService.AddIngredientsToNewMedicine(items, ingredient);
        }
        public List<Ingredient> GetAllIngredients()
        {
            return _medicineIngredientsService.GetAllIngredients();
        }
    }
}
