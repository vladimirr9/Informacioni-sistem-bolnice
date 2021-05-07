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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ObavjestenjaPacijent.xaml
    /// </summary>
    public partial class ObavjestenjaPacijent : Window
    {
        private Pacijent pacijent;
        public ObavjestenjaPacijent(Pacijent p)
        {
            this.pacijent = p;
            InitializeComponent();
            imePacijenta.Text = p.ime + " " + p.prezime;
            this.DataContext = this;
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            DateTime now = DateTime.Now;
            foreach (Terapija t in pacijent.zdravstveniKarton.Terapija) {
                if (t.PocetakTerapije < now && t.KrajTerapije > now) {

                        PrikazObavjestenja.Items.Add(t.Opis);
                }
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            PocetnaPacijent m = new PocetnaPacijent(this.pacijent);
            Application.Current.MainWindow = m;
            m.Show();
            this.Close();
            return;
        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            Application.Current.MainWindow = m;
            m.Show();
            this.Close();
            return;
        }
    }
}
