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
    /// Interaction logic for ObavjestenjaPage.xaml
    /// </summary>
    public partial class ObavjestenjaPage : Page
    {

        private PocetnaPacijent parent;
        public ObavjestenjaPage(PocetnaPacijent pp)
        {
            parent = pp;
            InitializeComponent();
            updateVisibility();
            this.DataContext = this;
            LoadNotifications();
        }
        private void LoadNotifications()
        {
            DateTime now = DateTime.Now;
            foreach (Terapija t in parent.Pacijent.zdravstveniKarton.Terapija)
            {
                if (t.PocetakTerapije < now && t.KrajTerapije > now)
                {

                    PrikazObavjestenja.Items.Add(t.Opis);
                }
            }
        }



        private void updateVisibility()
        {
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.imePacijenta.Visibility = Visibility.Visible;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new StartPacijentPage(parent);
        }
    }
}
