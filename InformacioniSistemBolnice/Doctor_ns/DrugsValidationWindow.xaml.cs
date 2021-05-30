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
                String name = DrugsList.SelectedItem.ToString();
                List<Medicine> drugs = MedicineFileRepository.GetAll();
                foreach (Medicine drug in drugs)
                {
                    if (drug.Name == name)
                    {
                        drug.MedicineStatus = MedicineStatus.validiran;

                        //update lek storage!
                    }
                }
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
            List<Medicine> drugs = MedicineFileRepository.GetAll();
            foreach (Medicine drug in drugs)
            {
                if (!drug.IsDeleted && drug.MedicineStatus.Equals(MedicineStatus.waitingForValidation))
                    DrugsList.Items.Add(drug.Name);
            }
        }

        public void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            IngredientsList.Items.Clear();
            String name = DrugsList.SelectedItem.ToString();
            List<Medicine> drugs = MedicineFileRepository.GetAll();
            foreach (Medicine drug in drugs)
            {
                if (drug.Name == name)
                {
                    Console.WriteLine(name);
                    foreach (Ingredient ingredient in drug.IngredientsList)
                    {
                        IngredientsList.Items.Add(ingredient.Name);
                    }
                }
            }
        }
    }
}
