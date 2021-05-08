using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for PocetnaPacijent.xaml
    /// </summary>
    public partial class PocetnaPacijent : Window
    {

        public Pacijent Pacijent { get; set; }
        public PocetnaPacijent(Pacijent pacijent)
        {
            this.Pacijent = pacijent;
            InitializeComponent();
            imePacijenta.Text = pacijent.ime + " " + pacijent.prezime;
            
            
        }

        private void odjava_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            Application.Current.MainWindow = m;
            m.Show();
            this.Close();
            return;
        }

        private void pregledTermina_Click(object sender, RoutedEventArgs e)
        {
            PacijentWindow pw = new PacijentWindow(this.Pacijent);
            Application.Current.MainWindow = pw;
            pw.Show();
            this.Close();
            return;
        }

        private void obavjestenja_Click(object sender, RoutedEventArgs e)
        {
            ObavjestenjaPacijent op = new ObavjestenjaPacijent(this.Pacijent);
            Application.Current.MainWindow = op;
            op.Show();
            this.Close();
            return;
        }

        private void ocjenjivanje_Click(object sender, RoutedEventArgs e)
        {
            Ocjenjivanje o = new Ocjenjivanje(this);
            Application.Current.MainWindow = o;
            o.Show();
            this.Close();
            return;
        }
    }
}
