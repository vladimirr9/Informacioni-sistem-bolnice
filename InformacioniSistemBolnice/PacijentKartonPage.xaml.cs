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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PacijentKartonPage.xaml
    /// </summary>
    public partial class PacijentKartonPage : Page
    {
        private static PocetnaPacijent parent;
        public PacijentKartonPage(PocetnaPacijent pp)
        {
            parent = pp;
            InitializeComponent();
            borderWindow.Content = new InformacijeKartonPage(pp);
        }


        private void informacijeKarton_Click(object sender, RoutedEventArgs e)
        {
            borderWindow.Content = new InformacijeKartonPage(parent);
        }

        private void terapijaKarton_Click(object sender, RoutedEventArgs e)
        {
            borderWindow.Content = new TerapijaKartonPage();
        }

        private void istorijaBolesti_Click(object sender, RoutedEventArgs e)
        {
            borderWindow.Content = new IstorijaBolestiPage();
        }

        private void pregledAnamneza_Click(object sender, RoutedEventArgs e)
        {
            borderWindow.Content = new PregledAnamnezaPage();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new StartPacijentPage(parent);
        }
    }
}
