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
    /// Interaction logic for LekoviWindow.xaml
    /// </summary>
    public partial class LekoviWindow : Window
    {
        private UpravnikWindow parent;
        public LekoviWindow(UpravnikWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            updateTable();

            this.DataContext = this;
            SastojciLeka.Items.Clear();
        }

        private void ObrisiLek(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedItem != null)
            {
                MessageBoxResult odgovor = MessageBox.Show("Da li želite da obrišete selektovani lek?", "Potvrda brisanja leka", MessageBoxButton.YesNo);
                if (odgovor == MessageBoxResult.Yes)
                {
                    Lek selektovan = (Lek)dataGridLekovi.SelectedItem;
                    selektovan.StatusLeka = StatusLeka.cekaNaValidaciju;
                    //LekFileStorage.RemoveLek(selektovan.Naziv);
                    //dataGridLekovi.Items.Remove(dataGridLekovi.SelectedItem);
                    updateTable();
                }
            }
        }

        private void IzmeniLek(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedItem != null)
            {
                Lek l = (Lek)dataGridLekovi.SelectedItem;
                if (l.StatusLeka == StatusLeka.validiran || l.StatusLeka == StatusLeka.odbijen)
                {
                    Lek lekZaIzmenu = LekFileStorage.GetOne(l.Sifra);
                    IzmenaLekaWindow prozor = new IzmenaLekaWindow(lekZaIzmenu, this);
                    prozor.Show();
                }
                else
                {
                    MessageBox.Show("Ne možete menjati lek koji je na čekanju za validaciju.", "Upozorenje!", MessageBoxButton.OK);
                }

            }
        }

        private void DodajNoviLek(object sender, RoutedEventArgs e)
        {
            DodavanjeLekaWindow prozor = new DodavanjeLekaWindow(this);
            prozor.Show();
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void updateTable()
        {
            dataGridLekovi.Items.Clear();
            List<Lek> lekovi = LekFileStorage.GetAll();
            foreach (Lek l in lekovi)
            {
                if (!l.IsDeleted)
                    dataGridLekovi.Items.Add(l);
            }
        }

        /*private void IspisiSastojke(Lek lek)
        {
            SastojciLeka.Items.Clear();
            //List<Ingredient> sastojci = new List<Ingredient>();
            foreach (Ingredient sastojak in IngredientFileStorage.GetAll())
            {
                if (lek.ListaSastojaka.Contains(sastojak))
                {
                    SastojciLeka.Items.Add(sastojak);
                }
                /*else
                {
                    sastojci.Add(sastojak);
                }

            }
            //SastojciLeka.ItemsSource = sastojci;
        }*/

        private void dataGridLekovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SastojciLeka.Items.Clear();
            Lek selektovani = (Lek)dataGridLekovi.SelectedItem;
            /*foreach (Lek lek in LekFileStorage.GetAll())
            {
                if (selektovani == lek)
                {
                    SastojciLeka.Items.Add(lek.;
                }
            }*/
            foreach(Ingredient s in selektovani.ListaSastojaka)
            {
                SastojciLeka.Items.Add(s.Name);
            }
        }
    }
}
