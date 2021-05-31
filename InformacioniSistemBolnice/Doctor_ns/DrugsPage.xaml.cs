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
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class DrugsPage : Page
    {
        public DoctorWindow parent;
        private static DrugsPage instance;
        private MedicineController _medicineController = new MedicineController();

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

        private void For_Confirmation_Click(object sender, RoutedEventArgs e)
        {
            DrugsValidationWindow drugsWindow = new DrugsValidationWindow();
            Application.Current.MainWindow = drugsWindow;
            drugsWindow.Show();
        }

        private void Add_Ingredient_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsComboBox.SelectedIndex != -1)
            {
                foreach (Medicine drug in _medicineController.GetAllMedicines())
                {
                    if (drug.Name == DrugsList.SelectedItem.ToString())
                    {
                        drug.IngredientsList.Add((Ingredient)IngredientsComboBox.SelectedItem);
                        _medicineController.UpdateMedicine(drug);
                        WriteIngredients(drug);
                    }
                }
            }
        }

        private void UpdateList()
        {
            DrugsList.Items.Clear();
            foreach (Medicine drug in _medicineController.GetValidatedMedicines())
            {
                DrugsList.Items.Add(drug.Name);
            }
        }

        public void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            IngredientsList.Items.Clear();
            foreach (Medicine drug in _medicineController.GetValidatedMedicines())
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
            foreach (Ingredient ingredient in _medicineController.GetAllIngredients())
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
