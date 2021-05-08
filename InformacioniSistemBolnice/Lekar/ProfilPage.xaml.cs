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

namespace InformacioniSistemBolnice.Lekar
{
    /// <summary>
    /// Interaction logic for ProfilPage.xaml
    /// </summary>
    public partial class ProfilPage : Page
    {

        public LekarWindow parent;
        public global::Lekar lekar;
        private static ProfilPage instance;
        public ProfilPage(LekarWindow parent)
        {
            this.parent = parent;
            this.lekar = parent.lekar;
            InitializeComponent();
            FillLabels();
        }

        public static ProfilPage GetPage(LekarWindow parent)
        {
            if (instance == null)
                instance = new ProfilPage(parent);
            return instance;
        }

        private void FillLabels()
        {
            Ime.Content = lekar.ime + " " + lekar.prezime;
            Zvanje.Content = lekar.tipLekara.ToString();
            Datum.Content = lekar.datumRodenja.Date;
            JMBG.Content = lekar.jmbg;
            //Mesto.Content = lekar.adresaStanovanja.mestoStanovanja.ToString();
            Email.Content = lekar.email;
        }
    }
}
