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
    /// Interaction logic for InformacijeKartonPage.xaml
    /// </summary>
    public partial class InformacijeKartonPage : Page
    { 
         private static PocetnaPacijent pp;
        private static Pacijent pacijent;
        public InformacijeKartonPage(PocetnaPacijent p)
        {
            pp = p;
            pacijent = pp.Pacijent;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            prezimeTextBox.Text = pacijent.prezime;
            prezimeTextBox.IsEnabled = false;
            imeTextBox.Text = pacijent.ime;
            imeTextBox.IsEnabled = false;
            JMBGTextBox.Text = pacijent.jmbg;
            JMBGTextBox.IsEnabled = false;
            datumRodjenjaDatePicker.SelectedDate = pacijent.datumRodenja.Date;
            datumRodjenjaDatePicker.IsEnabled = false;
            kontaktTextBox.Text = pacijent.brojTelefona;
            kontaktTextBox.IsEnabled = false;
            emailTextBox.Text = pacijent.email;
            emailTextBox.IsEnabled = false;
            brojKarticeTextBox.Text = pacijent.brojZdravstveneKartice;
            brojKarticeTextBox.IsEnabled = false;
            brojKartonaTextBox.Text = pacijent.zdravstveniKarton.brojZdravstvenogKartona;
            brojKartonaTextBox.IsEnabled = false;
            if (pacijent.pol.Equals('M'))
            {
                mRadioButton.IsChecked = true;
                mRadioButton.IsEnabled = false;
                zRadioButton.IsEnabled = false;
            }
            else
            {
                zRadioButton.IsChecked = true;
                mRadioButton.IsEnabled = false;
                zRadioButton.IsEnabled = false;
            }
            adresaTextBox.Text = pacijent.adresaStanovanja.ulicaIBroj + "," + pacijent.adresaStanovanja.mestoStanovanja;
            adresaTextBox.IsEnabled = false;
        }
}
}
