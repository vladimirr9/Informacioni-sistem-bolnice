using InformacioniSistemBolnice.Korisnik;
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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for IzmeniObavestenjeWindow.xaml
    /// </summary>
    public partial class IzmeniObavestenjeWindow : Window
    {
        private PocetnaPage parent;
        private Obavestenje staroObavestenje;
        public IzmeniObavestenjeWindow(PocetnaPage parent, Obavestenje staroObavestenje)
        {
            this.parent = parent;
            this.staroObavestenje = staroObavestenje;
            InitializeComponent();

            Naslov.Text = staroObavestenje.naslov;
            Sadrzaj.Text = staroObavestenje.tekst;

        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            int id = staroObavestenje.idObavestenja;
            String naslov = Naslov.Text;
            String sadrzaj = Sadrzaj.Text;
            DateTime datumIVreme = new DateTime();
            datumIVreme = DateTime.Now;

            Obavestenje novoObavestenje = new Obavestenje(id, naslov, sadrzaj, datumIVreme);
            ObavestenjeFileStorage.UpdateObavestenje(staroObavestenje.idObavestenja, novoObavestenje);
            parent.updateTable();
            Close();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
