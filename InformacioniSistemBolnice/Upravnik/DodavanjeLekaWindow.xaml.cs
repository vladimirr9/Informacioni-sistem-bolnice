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
            List<global::Lekar> lekari = LekarFileStorage.GetAll();

            Lekar.ItemsSource = lekari;
            List<Sastojak> sastojci = SastojakFileStorage.GetAll();
            Sastojci.ItemsSource = sastojci;
        }

        private void DodajLek(object sender, RoutedEventArgs e)
        {
            String sifra = Sifra.Text;
            String naziv = Naziv.Text;
            StatusLeka statusLeka = StatusLeka.cekaNaValidaciju;
            bool isDeleted = false;
            global::Lekar lekar = (global::Lekar)Lekar.SelectedItem;
            List<Sastojak> sastojciSvi = SastojakFileStorage.GetAll();
            List<Sastojak> sastojciLeka = new List<Sastojak>();
            String[] naziviSastojaka1 = Sastojci.Text.Split(',');
            /*foreach (Sastojak s in sastojciSvi)
            {
                Sastojak noviSastojak = new Sastojak(0, "", false);

                for (int i = 0; i < naziviSastojaka1.Length; i++)
                {
                    if (naziviSastojaka1[i].Equals(s.naziv))
                    {
                        noviSastojak.id = s.id;
                        noviSastojak.naziv = naziviSastojaka1[i];
                        noviSastojak.isDeleted = false;
                    }
                }

                sastojciLeka.Add(noviSastojak);
            }*/

            Lek l = new Lek(sifra, naziv, isDeleted, statusLeka, sastojciLeka);
            LekFileStorage.AddLek(l);
            MessageBox.Show("Lek poslat lekaru na validaciju!", "Čekanje na validaciju", MessageBoxButton.OK);
            //parent.UpdateTable();
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
