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

namespace InformacioniSistemBolnice.Doctor_ns
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
            DrugsValidationWindow drugsWindow = new DrugsValidationWindow();
            Application.Current.MainWindow = drugsWindow;
            drugsWindow.Show();
        }

        private void DodajSastojakClick(object sender, RoutedEventArgs e)      //ubaciti kontroler za lekove
        {
            if (IngredientsComboBox.SelectedIndex != -1)
            {
                List<Medicine> drugs = MedicineFileRepository.GetAll();
                foreach (Medicine drug in drugs)
                {
                    if (drug.Name == DrugsList.SelectedItem.ToString())
                    {
                        drug.IngredientsList.Add((Ingredient)IngredientsComboBox.SelectedItem);
                        MedicineFileRepository.UpdateMedicine(drug.MedicineId, drug);
                        WriteIngredients(drug);
                    }
                }
            }
        }

        private void UpdateList()
        {
            DrugsList.Items.Clear();
            List<Medicine> drugs = MedicineFileRepository.GetAll();                    //kontroler
            foreach (Medicine drug in drugs)
            {
                if (!drug.IsDeleted && drug.MedicineStatus.Equals(MedicineStatus.validiran))
                    DrugsList.Items.Add(drug.Name);
            }
        }

        public void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            IngredientsList.Items.Clear();
            foreach (Medicine drug in MedicineFileRepository.GetAll())                        //kontroler
            {
                if (drug.Name == DrugsList.SelectedItem.ToString())
                {
                    WriteIngredients(drug);
                }
            }
        }

        private void WriteIngredients(Medicine drug)
        {
            IngredientsList.Items.Clear();
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (Ingredient ingredient in IngredientFileStorage.GetAll())
            {
                if (drug.IngredientsList.Contains(ingredient))
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
