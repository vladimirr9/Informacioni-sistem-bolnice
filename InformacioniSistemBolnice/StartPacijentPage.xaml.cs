using InformacioniSistemBolnice.FileStorage;
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
    /// Interaction logic for StartPacijentPage.xaml
    /// </summary>
    public partial class StartPacijentPage : Page
    {

        private static PocetnaPacijent parent;
        public StartPacijentPage(PocetnaPacijent p)
        {
            parent = p;
            InitializeComponent();
        }

        

        
    

        private void pregledTermina_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PregledTerminaPage(parent);  
        }

        private void obavjestenja_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new ObavjestenjaPage(parent);
        }

        private void ocjenjivanje_Click(object sender, RoutedEventArgs e)
        {
            PacijentFileStorage.ProvjeritiStatusPacijenta(parent.Pacijent);
            if (parent.Pacijent.Banovan == true)
            {
                MessageBox.Show("Ova funkcionalnost Vam je trenutno onemogućena,obratite se sekretaru!", "Greška");
            }
            else
            {
                parent.startWindow.Content = new AnketaPage(parent);
            }
        }

        private void karton_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PacijentKartonPage(parent);
        }
    }
    
}

