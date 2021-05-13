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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for PacijentiPage.xaml
    /// </summary>
    public partial class PacijentiPage : Page
    {
        private static PacijentiPage instance;
        private PacijentiPage()
        {
            InitializeComponent();
            updateTable();
        }
        public static PacijentiPage GetPage()
        {
            if (instance == null)
                instance = new PacijentiPage();
            else
                instance.updateTable();
            return instance;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjePacijenta window = new DodavanjePacijenta(this);
            window.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (PrikazPacijenata.SelectedItem != null)
            {
                Pacijent inicijalniPacijent = PacijentFileStorage.GetOne(((Pacijent)(PrikazPacijenata.SelectedItem)).korisnickoIme);
                IzmenaPacijenta window = new IzmenaPacijenta(inicijalniPacijent, this);
                window.Show();
            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {

            if (PrikazPacijenata.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da obrišete ovog pacijenta?", "Potvrda brisanja", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    PacijentFileStorage.RemovePacijent(((Pacijent)(PrikazPacijenata.SelectedItem)).korisnickoIme);
                    updateTable();
                }
            }
        }
        public void updateTable()
        {
            PrikazPacijenata.Items.Clear();
            List<Pacijent> pacijenti = PacijentFileStorage.GetAll();
            foreach (Pacijent p in pacijenti)
            {
                if (!p.isDeleted)
                    PrikazPacijenata.Items.Add(p);
            }
        }

        private void Detaljnije_Click(object sender, RoutedEventArgs e)
        {
            if (PrikazPacijenata.SelectedItem != null)
            {
                DetaljnijeWindow window = new DetaljnijeWindow(this, ((Pacijent)(PrikazPacijenata.SelectedItem)).korisnickoIme);
                window.Show();
            }
        }
    }
}
