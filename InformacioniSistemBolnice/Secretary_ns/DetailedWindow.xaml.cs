using System.Collections.Generic;
using System.Windows;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class DetailedWindow : Window
    {
        private Patient _patient;
        private PatientController _patientController = new PatientController();

        public DetailedWindow(PatientsPage parent, string username)
        {
            _patient = PatientFileRepository.GetOne(username);
            InitializeComponent();
            DataContext = _patient;
            UpdateTable();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllergensCombo.SelectedItem == null)
                return;
            Ingredient ingredient = (Ingredient)AllergensCombo.SelectedItem;
            _patientController.AddAllergen(_patient, ingredient);
            UpdateTable();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllergenListView.SelectedItem == null)
                return;

            var result = MessageBox.Show("Da li ste sigurni da želite da obrišete ovaj alergen?", "Potvrda brisanja",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            Ingredient ingredient = (Ingredient)AllergenListView.SelectedItem;
            _patientController.RemoveAllergen(_patient, ingredient);
            UpdateTable();
        }

        public void UpdateTable()
        {
            AllergenListView.Items.Clear();
            var allergensComboList = new List<Ingredient>();
            foreach (var ingredient in IngredientFileRepository.GetAll())
                if (_patient.MedicalRecord.Allergens.Contains(ingredient))
                    AllergenListView.Items.Add(ingredient);
                else
                    allergensComboList.Add(ingredient);
            AllergensCombo.ItemsSource = allergensComboList;
        }
    }
}