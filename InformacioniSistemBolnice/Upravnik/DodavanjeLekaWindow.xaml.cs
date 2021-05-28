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
    /// Interaction logic for DodavanjeLekaWindow.xaml
    /// </summary>
    public partial class DodavanjeLekaWindow : Window
    {
        LekoviWindow parent;
        public DodavanjeLekaWindow(LekoviWindow parent)
        {
            InitializeComponent();
            this.parent = parent;

            List<Ingredient> ingredients = IngredientFileStorage.GetAll();
            Sastojci.ItemsSource = ingredients;
            SastojciList.Items.Clear();
        }

        private void DodajLek(object sender, RoutedEventArgs e)
        {
            String sifra = Sifra.Text;
            String naziv = Naziv.Text;
            MedicineStatus statusLeka = MedicineStatus.waitingForValidation;
            bool isDeleted = false;
            ObservableCollection<Ingredient> ingredients = (ObservableCollection<Ingredient>)SastojciList.ItemsSource;
            List<Ingredient> sastojciLeka = ingredients.ToList();

            Medicine medicine = new Medicine(sifra, naziv, isDeleted, statusLeka, sastojciLeka);
            MedicineFileRepository.AddMedicine(medicine);
            MessageBox.Show("Lek poslat lekaru na validaciju!", "Čekanje na validaciju", MessageBoxButton.OK);
            parent.updateTable();
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddIngredient(object sender, RoutedEventArgs e)
        {
            //SastojciList.Items.Clear();
            Ingredient selected = (Ingredient)Sastojci.SelectedItem;
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
            foreach(Ingredient i in IngredientFileStorage.GetAll())
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
            // SastojciList.Items.Clear();
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
