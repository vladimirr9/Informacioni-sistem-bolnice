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

namespace InformacioniSistemBolnice.Lekar
{
    /// <summary>
    /// Interaction logic for LekoviPage.xaml
    /// </summary>
    public partial class LekoviPage : Page
    {
        public LekarWindow parent;
        private static LekoviPage instance;

        public LekoviPage(LekarWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            UpdateList();
            SastavList.Items.Clear();
        }

        public static LekoviPage GetPage(LekarWindow parent)
        {
            if (instance == null)
                instance = new LekoviPage(parent);
            return instance;
        }

        private void LekoviZaPotvrduClick(object sender, RoutedEventArgs e)
        {
            LekoviZaPotvrduWindow lekoviWindow = new LekoviZaPotvrduWindow();
            Application.Current.MainWindow = lekoviWindow;
            lekoviWindow.Show();
        }

        private void DodajSastojakClick(object sender, RoutedEventArgs e)
        {
            if (Sastojci.SelectedIndex != -1)
            {
                List<Lek> lekovi = LekFileStorage.GetAll();
                foreach (Lek l in lekovi)
                {
                    if (l.Naziv == LekoviList.SelectedItem.ToString())
                    {
                        l.ListaSastojaka.Add((Sastojak)Sastojci.SelectedItem);
                        //dodati u storage
                        IspisiSastojke(l);
                    }
                }
            }
        }

        private void UpdateList()
        {
            LekoviList.Items.Clear();
            List<Lek> lekovi = LekFileStorage.GetAll();
            foreach (Lek l in lekovi)
            {
                if (!l.IsDeleted && l.StatusLeka.Equals(StatusLeka.validiran))
                    LekoviList.Items.Add(l.Naziv);
            }
        }

        public void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            SastavList.Items.Clear();
            foreach (Lek lek in LekFileStorage.GetAll())
            {
                if (lek.Naziv == LekoviList.SelectedItem.ToString())
                {
                    IspisiSastojke(lek);
                }
            }
        }

        private void IspisiSastojke(Lek lek)
        {
            SastavList.Items.Clear();
            List<Sastojak> sastojci = new List<Sastojak>();
            foreach (Sastojak sastojak in SastojakFileStorage.GetAll())
            {
                if (lek.ListaSastojaka.Contains(sastojak))
                {
                    SastavList.Items.Add(sastojak.naziv);
                }
                else
                {
                    sastojci.Add(sastojak);
                }

            }
            Sastojci.ItemsSource = sastojci;
        }
    }
}
