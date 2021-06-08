using InformacioniSistemBolnice.FileStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Service
{
    public class MedicineService
    {
        public void AddMedicine(Medicine medicine)
        {
            if (!IsIdunique(medicine.MedicineId))
            {
                MessageBox.Show("Uneti ID leka već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            if (!IsNameUnique(medicine.Name))
            {
                MessageBox.Show("Uneto ime leka već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            MedicineFileRepository.AddMedicine(medicine);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            MedicineFileRepository.UpdateMedicine(medicine.MedicineId, medicine);
        }

        public void RemoveMedicine(Medicine medicine)
        {
            MedicineFileRepository.RemoveMedicine(medicine.MedicineId);
        }

        public void SendMedicineForRemovingValidation(Medicine medicine)
        {
            medicine.MedicineStatus = MedicineStatus.waitingForValidation;
            UpdateMedicine(medicine);
        }

        public Medicine GetOneByname(String name)
        {
            return MedicineFileRepository.GetOneByName(name);
        }
        public bool IsIdunique(String medicineId)
        {
            if (MedicineFileRepository.GetOne(medicineId) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsNameUnique(String name)
        {
            if (MedicineFileRepository.GetOne(name) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Medicine> GetAllMedicines()
        {
            return MedicineFileRepository.GetAll();
        }

        public List<Ingredient> GetMedicineIngredients(Medicine medicine)
        {
            List<Ingredient> ingredientsList = new List<Ingredient>();
            foreach (Ingredient ingredient in IngredientFileRepository.GetAll())
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
            /*var ingredients = new ObservableCollection<Ingredient>();
            var medIngredients = medicine.IngredientsList;
            foreach (var ing in medIngredients)
            {
                if (ing.Name.Equals(ingredient.Name))
                {
                    ingredients.Add(ing);
                }
            }
            return ingredients;*/
        }

        public void AddIngredientsToNewMedicine(ItemCollection items, Ingredient ingredient)
        {
            /*ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
            ingredientsList = ingredients;*/
            //items.Clear();
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
            return IngredientFileRepository.GetAll();
        }

        public int ValidatedMedicinesCount()
        {
            int sum = 0;
            foreach (Medicine medicine in GetAllMedicines())
            {
                if (medicine.MedicineStatus == MedicineStatus.validated)
                {
                    sum++;
                }
            }
            return sum;
        }
        public int RejectedMedicinesCount()
        {
            int sum = 0;
            foreach (Medicine medicine in GetAllMedicines())
            {
                if (medicine.MedicineStatus == MedicineStatus.rejected)
                {
                    sum++;
                }
            }
            return sum;
        }
        public int WaitingMedicinesCount()
        {
            int sum = 0;
            foreach (Medicine medicine in GetAllMedicines())
            {
                if (medicine.MedicineStatus == MedicineStatus.waitingForValidation)
                {
                    sum++;
                }
            }
            return sum;
        }
    }
}
