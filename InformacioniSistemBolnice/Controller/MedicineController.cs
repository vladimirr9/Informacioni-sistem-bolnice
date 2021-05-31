﻿using InformacioniSistemBolnice.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Controller
{
    public class MedicineController
    {
        private MedicineService _medicineService = new MedicineService();
        public void AddMedicine(Medicine medicine)
        {
            _medicineService.AddMedicine(medicine);
        }
        public void UpdateMedicine(Medicine medicine)
        {
            _medicineService.UpdateMedicine(medicine);
        }
        public void RemoveMedicine(Medicine medicine)
        {
            _medicineService.RemoveMedicine(medicine);
        }
        public Medicine GetOneByname(String name)
        {
            return _medicineService.GetOneByname(name);
        }
        public List<Medicine> GetAllMedicines()
        {
            return _medicineService.GetAllMedicines();
        }
        public List<Ingredient> GetMedicineIngredients(Medicine medicine)
        {
            return _medicineService.GetMedicineIngredients(medicine);
        }
        public ObservableCollection<Ingredient> AddIngredients(Medicine medicine, Ingredient ingredient)
        {
            return _medicineService.AddIngredients(medicine, ingredient);
        }
        public ObservableCollection<Ingredient> RemoveIngredient(Medicine medicine, Ingredient ingredient)
        {
            return _medicineService.RemoveIngredient(medicine, ingredient);
        }
        public ObservableCollection<Ingredient> RemoveIngredientFromNewMedicine(Ingredient ingredient)
        {
            return _medicineService.RemoveIngredientFromNewMedicine(ingredient);
        }

        public ObservableCollection<Ingredient> AddIngredientsToNewMedicine(Ingredient ingredient)
        {
            return _medicineService.AddIngredientsToNewMedicine(ingredient);
        }
        public List<Ingredient> GetAllIngredients()
        {
            return _medicineService.GetAllIngredients();
        }
        public void SendMedicineForRemovingValidation(Medicine medicine)
        {
            _medicineService.SendMedicineForRemovingValidation(medicine);
        }
    }
}