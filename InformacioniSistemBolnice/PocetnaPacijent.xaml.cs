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
    /// Interaction logic for PocetnaPacijent.xaml
    /// </summary>
    public partial class PocetnaPacijent : Window
    {

        public Pacijent pacijent { get; set; }
        public PocetnaPacijent(Pacijent pacijent)
        {
            this.pacijent = pacijent;
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
            PacijentWindow pw = new PacijentWindow(this.pacijent);
            Application.Current.MainWindow = pw;
            pw.Show();
            this.Close();
            return;
        }

       
    }
}
