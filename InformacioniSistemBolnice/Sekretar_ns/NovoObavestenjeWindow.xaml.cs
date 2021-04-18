using InformacioniSistemBolnice.Korisnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for NovoObavestenjeWindow.xaml
    /// </summary>
    public partial class NovoObavestenjeWindow : Window
    {
        private PocetnaPage parent;
        public NovoObavestenjeWindow(PocetnaPage parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            int id = ObavestenjeFileStorage.GetAll().Count;
            String naslov = Naslov.Text;
            String sadrzaj = Sadrzaj.Text;
            DateTime datumIVreme = new DateTime();
            datumIVreme = DateTime.Now;

            Obavestenje novoObavestenje = new Obavestenje(id, naslov, sadrzaj, datumIVreme);
            ObavestenjeFileStorage.AddObavestenje(novoObavestenje);
            parent.updateTable();
            Close();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
