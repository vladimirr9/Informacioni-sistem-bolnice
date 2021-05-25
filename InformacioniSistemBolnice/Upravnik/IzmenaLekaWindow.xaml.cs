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
        private Lek medForUpdate;
        public IzmenaLekaWindow(Lek med, LekoviWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            medForUpdate = med;

            List<Ingredient> sastojci = IngredientFileStorage.GetAll();
            Sastojci.ItemsSource = sastojci;

            Sifra.Text = medForUpdate.Sifra;
            Naziv.Text = medForUpdate.Naziv;
            SastojciList.ItemsSource = medForUpdate.ListaSastojaka;
            //StatusLeka statusLeka = medForUpdate.StatusLeka;
            //bool isDeleted = medForUpdate.IsDeleted;
        }

        private void UpdateMedicine(object sender, RoutedEventArgs e)
        {
            String sifra = Sifra.Text;
            String naziv = Naziv.Text;
            StatusLeka statusLeka = StatusLeka.cekaNaValidaciju;
            bool isDeleted = false;

            List<Ingredient> sastojciLeka = (List<Ingredient>)SastojciList.ItemsSource;
            Lek updatedMed = new Lek(sifra, naziv, isDeleted, statusLeka, sastojciLeka);
            LekFileStorage.UpdateLek(medForUpdate.Sifra, updatedMed);

            MessageBox.Show("Lek poslat lekaru na validaciju!", "Čekanje na validaciju", MessageBoxButton.OK);
            //_parent.UpdateTable();
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddIngredient(object sender, RoutedEventArgs e)
        {
            Ingredient selected = (Ingredient)Sastojci.SelectedItem;
            List<Ingredient> medIngredients = medForUpdate.ListaSastojaka;
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
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
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
