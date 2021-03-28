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
using InformacioniSistemBolnice.Lekar;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for LekarWindow.xaml
    /// </summary>
    public partial class LekarWindow : Window
    {
        public LekarWindow()
        {
            InitializeComponent();
            List<Termin> termini = new List<Termin>();
            foreach (var termin in termini)
            {

                PrikazPregleda.Items.Add(termin);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LekarDodajTerminWindow dodajWin = new LekarDodajTerminWindow();
            Application.Current.MainWindow = dodajWin;
            dodajWin.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LekarIzmeniTerminWindow izmeniWin = new LekarIzmeniTerminWindow();    //poslati selektovani termin
            Application.Current.MainWindow = izmeniWin;
            izmeniWin.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (PrikazPregleda.SelectedItem != null)
            {
                //TerminFileStorage.RemoveTermin(PrikazPregleda.SelectedItem.iDTermina);
                //PrikazPregleda.Items.Remove(PrikazPregleda.SelectedItem);
            }
        }
    }
}
