using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InformacioniSistemBolnice.Controller;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class DrugsValidationWindow : Window
    {
        private MedicineController _medicineController = new MedicineController();
        public DrugsValidationWindow()
        {
            InitializeComponent();
            UpdateList();
            IngredientsList.Items.Clear();
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            if (DrugsList.SelectedItem != null)
            {
                Medicine drug = _medicineController.GetOneByname(DrugsList.SelectedItem.ToString());
                drug.MedicineStatus = MedicineStatus.validated;
                _medicineController.UpdateMedicine(drug);
                UpdateList();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            EditBox.Clear();
            UpdateList();
        }

        private void UpdateList()
        {
            DrugsList.Items.Clear();
            foreach (Medicine drug in _medicineController.GetAllMedicines())
            {
                if (!drug.IsDeleted && drug.MedicineStatus.Equals(MedicineStatus.waitingForValidation))
                    DrugsList.Items.Add(drug.Name);
            }
        }

        public void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            IngredientsList.Items.Clear();
            Medicine drug = _medicineController.GetOneByname(DrugsList.SelectedItem.ToString());
            foreach (Ingredient ingredient in _medicineController.GetMedicineIngredients(drug))
            {
                IngredientsList.Items.Add(ingredient.Name);
            }

        }
    }
}
