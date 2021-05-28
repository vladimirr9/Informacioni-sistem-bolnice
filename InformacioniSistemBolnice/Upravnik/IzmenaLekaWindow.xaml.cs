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
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Upravnik
{
    /// <summary>
    /// Interaction logic for IzmenaLekaWindow.xaml
    /// </summary>
    public partial class IzmenaLekaWindow : Window
    {
        private LekoviWindow parent;
        private Medicine medForUpdate;
        public IzmenaLekaWindow(Medicine med, LekoviWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            medForUpdate = med;

            List<Ingredient> sastojci = IngredientFileStorage.GetAll();
            Sastojci.ItemsSource = sastojci;

            Sifra.Text = medForUpdate.Id;
            Naziv.Text = medForUpdate.Name;
            List<Ingredient> ingredientList = medForUpdate.IngredientsList;
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>(ingredientList);
            SastojciList.ItemsSource = ingredients;
            //StatusLeka statusLeka = medForUpdate.StatusLeka;
            //bool isDeleted = medForUpdate.IsDeleted;
        }

        private void UpdateMedicine(object sender, RoutedEventArgs e)
        {
            String sifra = Sifra.Text;
            String naziv = Naziv.Text;
            MedicineStatus statusLeka = MedicineStatus.waitingForValidation;
            bool isDeleted = false;
            ObservableCollection<Ingredient> ingredients = (ObservableCollection<Ingredient>)SastojciList.ItemsSource;
            List<Ingredient> sastojciLeka = ingredients.ToList();
            Medicine updatedMed = new Medicine(sifra, naziv, isDeleted, statusLeka, sastojciLeka);
            MedicineFileRepository.UpdateMedicine(medForUpdate.Id, updatedMed);

            MessageBox.Show("Lek poslat lekaru na validaciju!", "Čekanje na validaciju", MessageBoxButton.OK);
            parent.updateTable();
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddIngredient(object sender, RoutedEventArgs e)
        {
            Ingredient selected = (Ingredient)Sastojci.SelectedItem;
            List<Ingredient> medIngredients = medForUpdate.IngredientsList;
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>(medIngredients);
            /*var ingredients = new ObservableCollection<Ingredient>();
            var medIngredients = medForUpdate.ListaSastojaka;
            foreach(var ingredient in medIngredients)
            {
                ingredients.Add(ingredient);
            }*/
            foreach (Ingredient i in IngredientFileStorage.GetAll())
            {
                if (!SastojciList.Items.Contains(selected) && i.Name.Equals(selected.Name))
                {
                    ingredients.Add(i);
                }
            }
            SastojciList.ItemsSource = ingredients;
        }

        private void RemoveIngredient(object sender, RoutedEventArgs e)
        {
            Ingredient selected = (Ingredient)SastojciList.SelectedItem;
            List<Ingredient> medIngredients = medForUpdate.IngredientsList;
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>(medIngredients);
            foreach (Ingredient i in IngredientFileStorage.GetAll())
            {
                if (i.Name.Equals(selected.Name) && SastojciList.SelectedItem != null)
                {
                    ingredients.Remove(selected);
                }
            }
            SastojciList.ItemsSource = ingredients;
        }
    }
}
