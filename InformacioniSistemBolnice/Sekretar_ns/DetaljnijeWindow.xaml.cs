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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for DetaljnijeWindow.xaml
    /// </summary>
    public partial class DetaljnijeWindow : Window
    {
        private PacijentiPage parent;
        private String korisnickoIme;
        private Pacijent pacijent;
        private List<Alergen> alergeni;
        public DetaljnijeWindow(PacijentiPage parent, String korisnickoIme)
        {
            this.parent = parent;
            this.korisnickoIme = korisnickoIme;
            pacijent = PacijentFileStorage.GetOne(korisnickoIme);
            alergeni = pacijent.zdravstveniKarton.Alergen;
            InitializeComponent();
            updateTable();
            
        }

        private void Zatvori_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            AlergenDodavanje ad = new AlergenDodavanje(pacijent, this);
            ad.ShowDialog();
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (AlergeniList.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da obrišete ovaj alergen?", "Potvrda brisanja", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    pacijent.zdravstveniKarton.RemoveAlergen((Alergen)AlergeniList.SelectedItem);
                    PacijentFileStorage.UpdatePacijent(pacijent.korisnickoIme, pacijent);
                    updateTable();
                }
            }

        }
        public void updateTable()
        {
            AlergeniList.Items.Clear();
            foreach (Alergen a in pacijent.zdravstveniKarton.Alergen)
            {
                AlergeniList.Items.Add(a);
            }
        }
    }
}
