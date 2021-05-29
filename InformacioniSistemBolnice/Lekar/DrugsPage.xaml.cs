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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Lekar
{
    public partial class DrugsPage : Page
    {
        public DoctorWindow parent;
        private static DrugsPage instance;

        public DrugsPage(DoctorWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            UpdateList();
            IngredientsList.Items.Clear();
        }

        public static DrugsPage GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new DrugsPage(parent);
            return instance;
        }

        private void LekoviZaPotvrduClick(object sender, RoutedEventArgs e)
        {
            DrugsValidationWindow lekoviWindow = new DrugsValidationWindow();
            Application.Current.MainWindow = lekoviWindow;
            lekoviWindow.Show();
        }

        private void DodajSastojakClick(object sender, RoutedEventArgs e)
        {
            if (IngredientsComboBox.SelectedIndex != -1)
            {
                List<Lek> drugs = LekFileStorage.GetAll();
                foreach (Lek drug in drugs)
                {
                    if (drug.Naziv == DrugsList.SelectedItem.ToString())
                    {
                        drug.ListaSastojaka.Add((Ingredient)IngredientsComboBox.SelectedItem);
                        //dodati u storage
                        WriteIngredients(drug);
                    }
                }
            }
        }

        private void UpdateList()
        {
            DrugsList.Items.Clear();
            List<Lek> drugs = LekFileStorage.GetAll();
            foreach (Lek drug in drugs)
            {
                if (!drug.IsDeleted && drug.StatusLeka.Equals(StatusLeka.validiran))
                    DrugsList.Items.Add(drug.Naziv);
            }
        }

        public void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            IngredientsList.Items.Clear();
            foreach (Lek drug in LekFileStorage.GetAll())
            {
                if (drug.Naziv == DrugsList.SelectedItem.ToString())
                {
                    WriteIngredients(drug);
                }
            }
        }

        private void WriteIngredients(Lek drug)
        {
            IngredientsList.Items.Clear();
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (Ingredient ingredient in IngredientFileStorage.GetAll())
            {
                if (drug.ListaSastojaka.Contains(ingredient))
                {
                    IngredientsList.Items.Add(ingredient.Name);
                }
                else
                {
                    ingredients.Add(ingredient);
                }

            }
            IngredientsComboBox.ItemsSource = ingredients;
        }
    }
}
