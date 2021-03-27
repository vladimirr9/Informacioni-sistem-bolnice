using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String procitano = File.ReadAllText(@"pacijenti.json");
            List<Pacijent> pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(procitano);
            foreach (var pacijent in pacijenti)
            {
                if (ime.Text == pacijent.korisnickoIme & lozinka.Password == pacijent.lozinka)
                {
                    PacijentWindow p = new PacijentWindow();
                    Application.Current.MainWindow = p;
                    p.Show();
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("Neuspjesno logovanje!");
        }
    }
}
