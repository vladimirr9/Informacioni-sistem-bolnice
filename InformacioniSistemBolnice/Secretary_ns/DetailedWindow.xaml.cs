using System.Collections.Generic;
using System.Windows;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class DetailedWindow : Window
    {
        private List<Ingredient> _allergens;
        private PatientsPage _parent;
        private Pacijent _patient;
        private string _username;

        public DetailedWindow(PatientsPage parent, string username)
        {
            _parent = parent;
            _username = username;
            _patient = PacijentFileStorage.GetOne(username);
            _allergens = _patient.zdravstveniKarton.Alergen;
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
            _patient.zdravstveniKarton.AddSastojak((Ingredient) AllergensCombo.SelectedItem);
            PacijentFileStorage.UpdatePacijent(_patient.korisnickoIme, _patient);
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

            _patient.zdravstveniKarton.RemoveAlergen((Ingredient) AllergenListView.SelectedItem);
            PacijentFileStorage.UpdatePacijent(_patient.korisnickoIme, _patient);
            UpdateTable();
        }

        public void UpdateTable()
        {
            AllergenListView.Items.Clear();
            var allergensComboList = new List<Ingredient>();
            foreach (var ingredient in IngredientFileStorage.GetAll())
                if (_patient.zdravstveniKarton.Alergen.Contains(ingredient))
                    AllergenListView.Items.Add(ingredient);
                else
                    allergensComboList.Add(ingredient);
            AllergensCombo.ItemsSource = allergensComboList;
        }
    }
}