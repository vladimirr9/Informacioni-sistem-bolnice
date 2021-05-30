using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Upravnik
{
    /// <summary>
    /// Interaction logic for IzmenaLekaWindow.xaml
    /// </summary>
    public partial class IzmenaLekaWindow : Window
    {
        private LekoviWindow _parent;
        private Medicine _medForUpdate;
        private MedicineController _medicineController = new MedicineController();

        public IzmenaLekaWindow(Medicine med, LekoviWindow parent)
        {
            InitializeComponent();
            this._parent = parent;
            _medForUpdate = med;

            List<Ingredient> sastojci = _medicineController.GetAllIngredients();
            Sastojci.ItemsSource = sastojci;
            CollectSelectedMedicineData();
        }

        private void RemoveIngredient(object sender, RoutedEventArgs e)
        {
            Ingredient selected = (Ingredient)SastojciList.SelectedItem;
            /*List<Ingredient> medIngredients = _medForUpdate.IngredientsList;
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>(medIngredients);
            foreach (Ingredient i in IngredientFileStorage.GetAll())
            {
                if (i.Name.Equals(selected.Name) && SastojciList.SelectedItem != null)
                {
                    ingredients.Remove(selected);
                }
            }*/
            ObservableCollection<Ingredient> ingredients = _medicineController.RemoveIngredient(selected);
            SastojciList.ItemsSource = ingredients;
        }

        private void AddIngredient(object sender, RoutedEventArgs e)
        {
            Ingredient selected = (Ingredient)Sastojci.SelectedItem;
            //List<Ingredient> medIngredients = _medForUpdate.IngredientsList;
            /*ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>(medIngredients);
            var ingredients = new ObservableCollection<Ingredient>();
            var medIngredients = medForUpdate.ListaSastojaka;
            foreach(var ingredient in medIngredients)
            {
                ingredients.Add(ingredient);
            }*/
            /*foreach (Ingredient i in IngredientFileStorage.GetAll())
            {
                if (!SastojciList.Items.Contains(selected) && i.Name.Equals(selected.Name))
                {
                    ingredients.Add(i);
                }
            }*/
            ObservableCollection<Ingredient> ingredients = _medicineController.AddIngredientsToNewMedicine(selected);
            SastojciList.ItemsSource = ingredients;
        }

        private void UpdateMedicine(object sender, RoutedEventArgs e)
        {
            Medicine updatedMed = GenerateMedicineObjectFromCollectedData();
            //MedicineFileRepository.UpdateMedicine(_medForUpdate.MedicineId, updatedMed);
            _medicineController.UpdateMedicine(updatedMed);
            MessageBox.Show("Lek poslat lekaru na validaciju!", "Čekanje na validaciju", MessageBoxButton.OK);
            _parent.UpdateTable();
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void CollectSelectedMedicineData()
        {
            Sifra.Text = _medForUpdate.MedicineId;
            Naziv.Text = _medForUpdate.Name;
            List<Ingredient> ingredientList = _medForUpdate.IngredientsList;
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>(ingredientList);
            SastojciList.ItemsSource = ingredients;
        }
        public Medicine GenerateMedicineObjectFromCollectedData()
        {
            String medicineId = Sifra.Text;
            String name = Naziv.Text;
            MedicineStatus medicineStatus = MedicineStatus.waitingForValidation;
            bool isDeleted = false;
            ObservableCollection<Ingredient> ingredients = (ObservableCollection<Ingredient>)SastojciList.ItemsSource;
            List<Ingredient> medicineIngredients = ingredients.ToList();

            Medicine medicine = new Medicine(medicineId, name, isDeleted, medicineStatus, medicineIngredients);
            return medicine;
        }
    }
}
