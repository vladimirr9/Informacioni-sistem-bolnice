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
    /// Interaction logic for DodavanjeLekaWindow.xaml
    /// </summary>
    public partial class DodavanjeLekaWindow : Window
    {
        private LekoviWindow _parent;
        private MedicineController _medicineController = new MedicineController();
        private MedicineIngredientsController _medicineIngredientsController = new MedicineIngredientsController();
        public DodavanjeLekaWindow(LekoviWindow parent)
        {
            InitializeComponent();
            this._parent = parent;

            Sastojci.ItemsSource = _medicineIngredientsController.GetAllIngredients();
            SastojciList.Items.Clear();
        }

        private void DodajLek(object sender, RoutedEventArgs e)
        {
            Medicine newMedicine = GenerateMedicineObjectFromCollectedData();
            _medicineController.AddMedicine(newMedicine);
            MessageBox.Show("Lek poslat lekaru na validaciju!", "Čekanje na validaciju", MessageBoxButton.OK);
            _parent.UpdateTable();
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddIngredient(object sender, RoutedEventArgs e)
        {
            Ingredient selected = (Ingredient)Sastojci.SelectedItem;
            /*ObservableCollection<Ingredient> ingredientsList = (ObservableCollection<Ingredient>)SastojciList.ItemsSource;
            ObservableCollection<Ingredient> ingredients = _medicineController.AddIngredientsToNewMedicine(ingredieSastojciListntsList, selected);
            SastojciList.ItemsSource = ingredients;*/
            _medicineIngredientsController.AddIngredientsToNewMedicine(SastojciList.Items, selected);
        }

        private void RemoveIngredient(object sender, RoutedEventArgs e)
        {
            // SastojciList.Items.Clear();
            Ingredient selected = (Ingredient)SastojciList.SelectedItem;
            ObservableCollection<Ingredient> ingredients = _medicineIngredientsController.RemoveIngredientFromNewMedicine(selected);
            SastojciList.ItemsSource = ingredients;
        }

        public Medicine GenerateMedicineObjectFromCollectedData()
        {
            String medicineId = Sifra.Text;
            String name = Naziv.Text;
            MedicineStatus medicineStatus = MedicineStatus.waitingForValidation;
            bool isDeleted = false;
            int quantity = Convert.ToInt32(Kolicina.Text);
            List<Ingredient> ingredients = (List<Ingredient>)SastojciList.ItemsSource;
            //List<Ingredient> medicineIngredients = ingredients.ToList();

            Medicine medicine = new Medicine(medicineId, name, isDeleted, medicineStatus, quantity, ingredients);
            return medicine;
        }
    }
    /*ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
    foreach (Ingredient i in IngredientFileStorage.GetAll())
    {
        if (!SastojciList.Items.Contains(selected) && i.Name.Equals(selected.Name))
        {
            ingredients.Add(i);
        }
    }*/
    /*ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
    foreach (Ingredient i in IngredientFileStorage.GetAll())
    {
        if (i.Name.Equals(selected.Name) && SastojciList.SelectedItem != null)
        {
            ingredients.Remove(selected);
        }
    }*/
}
