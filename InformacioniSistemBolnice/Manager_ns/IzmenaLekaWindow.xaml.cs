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
        private MedicineIngredientsController _medicineIngredientsController = new MedicineIngredientsController();

        public IzmenaLekaWindow(Medicine med, LekoviWindow parent)
        {
            InitializeComponent();
            this._parent = parent;
            _medForUpdate = med;

            List<Ingredient> ingredients = _medicineIngredientsController.GetAllIngredients();
            Sastojci.ItemsSource = ingredients;
            CollectSelectedMedicineData();
        }

        private void RemoveIngredient(object sender, RoutedEventArgs e)
        {
            Ingredient selected = (Ingredient)SastojciList.SelectedItem;
            ObservableCollection<Ingredient> ingredients = _medicineIngredientsController.RemoveIngredient(_medForUpdate ,selected);
            SastojciList.ItemsSource = ingredients;
        }

        private void AddIngredient(object sender, RoutedEventArgs e)
        {
            Ingredient selected = (Ingredient)Sastojci.SelectedItem;           
            ObservableCollection<Ingredient> ingredients = _medicineIngredientsController.AddIngredients(_medForUpdate, selected);
            SastojciList.ItemsSource = ingredients;
        }

        private void UpdateMedicine(object sender, RoutedEventArgs e)
        {
            Medicine updatedMed = GenerateMedicineObjectFromCollectedData();
            //MedicineFileRepository.Update(_medForUpdate.MedicineId, updatedMed);
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
            Kolicina.Text = _medForUpdate.Quantity.ToString();
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>(ingredientList);
            SastojciList.ItemsSource = ingredients;
        }
        public Medicine GenerateMedicineObjectFromCollectedData()
        {
            String medicineId = Sifra.Text;
            String name = Naziv.Text;
            MedicineStatus medicineStatus = MedicineStatus.waitingForValidation;
            bool isDeleted = false;
            int quantity = Convert.ToInt32(Kolicina.Text);
            ObservableCollection<Ingredient> ingredients = (ObservableCollection<Ingredient>)SastojciList.ItemsSource;
            List<Ingredient> medicineIngredients = ingredients.ToList();

            Medicine medicine = new Medicine(medicineId, name, isDeleted, medicineStatus, quantity, medicineIngredients);
            return medicine;
        }
    }
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
    /*List<Ingredient> medIngredients = _medForUpdate.IngredientsList;
    ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>(medIngredients);
    foreach (Ingredient i in IngredientFileStorage.GetAll())
    {
        if (i.Name.Equals(selected.Name) && SastojciList.SelectedItem != null)
        {
            ingredients.Remove(selected);
        }
    }*/
}
