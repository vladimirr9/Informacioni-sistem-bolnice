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
    /// Interaction logic for IzmenaLekaWindow.xaml
    /// </summary>
    public partial class IzmenaLekaWindow : Window
    {
        private LekoviWindow parent;
        private Lek lekZaIzmenu;
        public IzmenaLekaWindow(Lek l, LekoviWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            lekZaIzmenu = l;

            String naziviSastojaka = lekZaIzmenu.ListaSastojaka.Select(x => x.naziv).ToArray().ToString();

            Sifra.Text = lekZaIzmenu.Sifra;
            Naziv.Text = lekZaIzmenu.Naziv;
            Sastojci.Text = naziviSastojaka;
            StatusLeka statusLeka = lekZaIzmenu.StatusLeka;
            bool isDeleted = lekZaIzmenu.IsDeleted;
        }

        private void IzmeniLek(object sender, RoutedEventArgs e)
        {
            String sifra = Sifra.Text;
            String naziv = Naziv.Text;
            StatusLeka statusLeka = StatusLeka.cekaNaValidaciju;
            bool isDeleted = false;
            global::Lekar lekar = (global::Lekar)Lekar.SelectedItem;
            List<Sastojak> sastojciSvi = SastojakFileStorage.GetAll();
            List<Sastojak> sastojciLeka = new List<Sastojak>();
            String[] naziviSastojaka1 = Sastojci.Text.Split(',');
            foreach (Sastojak s in sastojciSvi)
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
            }

            Lek l = new Lek(sifra, naziv, isDeleted, statusLeka, sastojciLeka);
            LekFileStorage.UpdateLek(lekZaIzmenu.Sifra, l);

            MessageBox.Show("Lek poslat lekaru na validaciju!", "Čekanje na validaciju", MessageBoxButton.OK);
            //parent.UpdateTable();
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IspisiSastojke(Lek lek)
        {
            SastojciList.Items.Clear();
            List<Sastojak> sastojci = new List<Sastojak>();
            foreach (Sastojak sastojak in SastojakFileStorage.GetAll())
            {
                if (lek.ListaSastojaka.Contains(sastojak))
                {
                    SastojciList.Items.Add(sastojak.naziv);
                }
                else
                {
                    sastojci.Add(sastojak);
                }

            }
            Sastojci.ItemsSource = sastojci;
        }

        private void Sastojci_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SastojciList.Items.Clear();
            foreach (Lek lek in LekFileStorage.GetAll())
            {
                if (lek.Naziv == lekZaIzmenu.Naziv)
                {
                    IspisiSastojke(lek);
                }
            }
        }
    }
}
