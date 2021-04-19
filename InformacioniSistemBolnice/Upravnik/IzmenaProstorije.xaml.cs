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

namespace InformacioniSistemBolnice.Upravnik
{
    /// <summary>
    /// Interaction logic for IzmenaProstorije.xaml
    /// </summary>
    public partial class IzmenaProstorije : Window
    {
        private Prostorija prostorijaZaIzmenu;
        private WindowProstorije parent;
        public IzmenaProstorije(Prostorija p, WindowProstorije parent)
        {
            prostorijaZaIzmenu = p;
            InitializeComponent();

            Naziv.Text = prostorijaZaIzmenu.Naziv;
            IDprostorije.Text = prostorijaZaIzmenu.IDprostorije.ToString();
            if (prostorijaZaIzmenu.TipProstorije == 0)
            {
                TipProstorije.SelectedIndex = 0;
            }
            else if (prostorijaZaIzmenu.TipProstorije == (TipProstorije)1)
            {
                TipProstorije.SelectedIndex = 1;
            }
            else
            {
                TipProstorije.SelectedIndex = 2;
            }
            //IsDeleted.IsChecked = prostorijaZaIzmenu.IsDeleted;
            IsActive.IsChecked = prostorijaZaIzmenu.IsActive;
            Kvadratura.Text = prostorijaZaIzmenu.Kvadratura.ToString();
            BrSprata.Text = prostorijaZaIzmenu.BrSprata.ToString();
            BrSobe.Text = prostorijaZaIzmenu.BrSobe.ToString();
            this.parent = parent;
        }

        private void Potvrda_Click(object sender, RoutedEventArgs e)
        {
            String naziv = Naziv.Text;
            int iDprostorije = Convert.ToInt32(IDprostorije.Text);
            TipProstorije tipProstorije; // = (TipProstorije)TipProstorije.SelectedItem;
            if (TipProstorije.SelectedIndex == 0)
            {
                tipProstorije = 0;
            }
            else if (TipProstorije.SelectedIndex == 1)
            {
                tipProstorije = (TipProstorije)1;
            }
            else
            {
                tipProstorije = (TipProstorije)2;
            }
            Boolean isDeleted = false;
            Boolean isActive = (Boolean)IsActive.IsChecked;
            Double kvadratura = Convert.ToDouble(Kvadratura.Text);
            int brSprata = Convert.ToInt32(BrSprata.Text);
            int brSobe = Convert.ToInt32(BrSobe.Text);
            List<Oprema> opremaLista = prostorijaZaIzmenu.OpremaLista;

            Prostorija p = new Prostorija(naziv, iDprostorije, tipProstorije, isDeleted, isActive, kvadratura, brSprata, brSobe, opremaLista);
            ProstorijaFileStorage.UpdateProstorija(prostorijaZaIzmenu.IDprostorije, p);
            parent.updateTable();
            Close();
        }

        /*private void isDeleted_Checked(object sender, RoutedEventArgs e)
        {

        }*/

        private void IsActive_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Odustanak_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
