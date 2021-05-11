using InformacioniSistemBolnice.FileStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;

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

        public void UpdateVisibilityOfComponents() {
            imePacijenta.Visibility = Visibility.Hidden;
            odjava.Visibility = Visibility.Hidden;
        }

       

        

       
    }
}
