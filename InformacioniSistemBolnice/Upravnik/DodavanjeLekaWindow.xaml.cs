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
            List<global::Doctor> lekari = LekarFileStorage.GetAll();

            Lekar.ItemsSource = lekari;
            List<Ingredient> sastojci = IngredientFileStorage.GetAll();
            Sastojci.ItemsSource = sastojci;
        }

        private void DodajLek(object sender, RoutedEventArgs e)
        {
            String sifra = Sifra.Text;
            String naziv = Naziv.Text;
            StatusLeka statusLeka = StatusLeka.cekaNaValidaciju;
            bool isDeleted = false;
            global::Doctor doctor = (global::Doctor)Lekar.SelectedItem;
            List<Ingredient> sastojciSvi = IngredientFileStorage.GetAll();
            List<Ingredient> sastojciLeka = new List<Ingredient>();
            String[] naziviSastojaka1 = Sastojci.Text.Split(',');
            /*foreach (Ingredient s in sastojciSvi)
            {
                Ingredient noviSastojak = new Ingredient(0, "", false);

                for (int i = 0; i < naziviSastojaka1.Length; i++)
                {
                    if (naziviSastojaka1[i].Equals(s.Name))
                    {
                        noviSastojak.ID = s.ID;
                        noviSastojak.Name = naziviSastojaka1[i];
                        noviSastojak.IsDeleted = false;
                    }
                }

                sastojciLeka.Add(noviSastojak);
            }*/

            Lek l = new Lek(sifra, naziv, isDeleted, statusLeka, sastojciLeka);
            LekFileStorage.AddLek(l);
            MessageBox.Show("Lek poslat lekaru na validaciju!", "Čekanje na validaciju", MessageBoxButton.OK);
            //_parent.UpdateTable();
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
