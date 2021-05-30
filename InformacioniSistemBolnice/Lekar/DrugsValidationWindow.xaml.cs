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

namespace InformacioniSistemBolnice.Lekar
{
    public partial class DrugsValidationWindow : Window
    {
        public DrugsValidationWindow()
        {
            InitializeComponent();
            UpdateList();
            IngredientsList.Items.Clear();
        }

        private void ValidirajClick(object sender, RoutedEventArgs e)
        {
            if (DrugsList.SelectedItem != null)
            {
                String name = DrugsList.SelectedItem.ToString();
                List<Lek> drugs = LekFileStorage.GetAll();
                foreach (Lek drug in drugs)
                {
                    if (drug.Naziv == name)
                    {
                        drug.StatusLeka = StatusLeka.validiran;

                        //update lek storage!
                    }
                }
                UpdateList();
            }
        }

        private void VratiClick(object sender, RoutedEventArgs e)
        {
            EditBox.Clear();
            UpdateList();
        }

        private void UpdateList()
        {
            DrugsList.Items.Clear();
            List<Lek> drugs = LekFileStorage.GetAll();
            foreach (Lek drug in drugs)
            {
                if (!drug.IsDeleted && drug.StatusLeka.Equals(StatusLeka.cekaNaValidaciju))
                    DrugsList.Items.Add(drug.Naziv);
            }
        }

        public void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            IngredientsList.Items.Clear();
            String name = DrugsList.SelectedItem.ToString();
            List<Lek> drugs = LekFileStorage.GetAll();
            foreach (Lek drug in drugs)
            {
                if (drug.Naziv == name)
                {
                    Console.WriteLine(name);
                    foreach (Ingredient ingredient in drug.ListaSastojaka)
                    {
                        IngredientsList.Items.Add(ingredient.Name);
                    }
                }
            }
        }
    }
}
