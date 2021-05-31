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
    /// Interaction logic for IzmenaLekaWindow.xaml
    /// </summary>
    public partial class IzmenaLekaWindow : Window
    {
        private LekoviWindow parent;
        private Medicine lekZaIzmenu;
        public IzmenaLekaWindow(Medicine l, LekoviWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            lekZaIzmenu = l;

            String naziviSastojaka = lekZaIzmenu.IngredientsList.Select(x => x.Name).ToArray().ToString();

            List<global::Doctor> lekari = DoctorFileRepository.GetAll();
            Lekar.ItemsSource = lekari;
            List<Ingredient> sastojci = IngredientFileRepository.GetAll();
            Sastojci.ItemsSource = sastojci;

            Sifra.Text = lekZaIzmenu.MedicineId;
            Naziv.Text = lekZaIzmenu.Name;
            SastojciList.ItemsSource = lekZaIzmenu.IngredientsList;
            MedicineStatus statusLeka = lekZaIzmenu.MedicineStatus;
            bool isDeleted = lekZaIzmenu.IsDeleted;
        }

        private void IzmeniLek(object sender, RoutedEventArgs e)
        {
            String sifra = Sifra.Text;
            String naziv = Naziv.Text;
            MedicineStatus statusLeka = MedicineStatus.waitingForValidation;
            bool isDeleted = false;
            global::Doctor doctor = (global::Doctor)Lekar.SelectedItem;
            /*List<Ingredient> sastojciSvi = IngredientFileRepository.GetAll();
            List<Ingredient> sastojciLeka = new List<Ingredient>();
            String[] naziviSastojaka1 = Sastojci.Text.Split(',');
            foreach (Ingredient s in sastojciSvi)
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
            List<Ingredient> sastojciLeka = (List<Ingredient>)SastojciList.ItemsSource;
            Medicine l = new Medicine(sifra, naziv, isDeleted, statusLeka, sastojciLeka);
            MedicineFileRepository.UpdateMedicine(lekZaIzmenu.MedicineId, l);

            MessageBox.Show("Lek poslat lekaru na validaciju!", "Čekanje na validaciju", MessageBoxButton.OK);
            //_parent.UpdateTable();
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Sastojci_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (Sastojci.SelectedIndex != -1)
            {
                List<Lek> lekovi = LekFileStorage.GetAll();
                foreach (Lek l in lekovi)
                {
                    l.ListaSastojaka.Add((Ingredient)Sastojci.SelectedItem);
                    //dodati u storage
                    foreach (Ingredient s in l.ListaSastojaka)
                    {
                        SastojciList.Items.Add(s.Name);
                    }

                }
            }*/
        }
    }
}
