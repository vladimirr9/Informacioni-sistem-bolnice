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
    /// Interaction logic for IzmenaOpreme.xaml
    /// </summary>
    public partial class IzmenaOpreme : Window
    {
        private OpremaWindow parent;
        private Oprema opremaZaIzmenu;
        private Prostorija selektovana;
        public IzmenaOpreme(Prostorija p, Oprema o, OpremaWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            opremaZaIzmenu = o;
            selektovana = p;

            IdProstorije.Text = selektovana.GetOne(opremaZaIzmenu.Sifra).IdProstorije.ToString();
            Sifra.Text = opremaZaIzmenu.Sifra;
            Naziv.Text = opremaZaIzmenu.Naziv;
            if (opremaZaIzmenu.TipOpreme == 0)
            {
                TipOpreme.SelectedIndex = 0;
            }
            else
            {
                TipOpreme.SelectedIndex = 1;
            }
            Kolicina.Text = opremaZaIzmenu.Kolicina.ToString();
            IsDeleted.IsChecked = opremaZaIzmenu.IsDeleted;
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IzmeniOpremu(object sender, RoutedEventArgs e)
        {
            int idProstorije = selektovana.GetOne(opremaZaIzmenu.Sifra).IdProstorije;
            String sifra = Sifra.Text;
            String naziv = Naziv.Text;
            TipOpreme tipOpreme;
            if (TipOpreme.SelectedIndex == 0)
            {
                tipOpreme = 0;
            }
            else
            {
                tipOpreme = (TipOpreme)1;
            }
            int kolicina = Convert.ToInt32(Kolicina.Text);
            Boolean isDeleted = (bool)IsDeleted.IsChecked;

            Oprema o = new Oprema(idProstorije, sifra, naziv, tipOpreme, kolicina, isDeleted);

            int idProstorije2 = selektovana.IDprostorije;
            String naziv2 = selektovana.Naziv;
            TipProstorije tipProstorije = selektovana.TipProstorije;
            Boolean isDeleted2 = selektovana.IsDeleted;
            Boolean isActive = selektovana.IsActive;
            Double kvadratura = selektovana.Kvadratura;
            int brSprata = selektovana.BrSprata;
            int brSobe = selektovana.BrSobe;
            List<Oprema> opremaLista = selektovana.OpremaLista;

            opremaLista.Remove(opremaZaIzmenu);
            opremaLista.Add(o);

            //Oprema o = new Oprema(sifra, naziv, tipOpreme, kolicina, isDeleted);
            //OpremaFileStorage.UpdateOprema(opremaZaIzmenu.Sifra, o);
            Prostorija p = new Prostorija(naziv2, idProstorije2, tipProstorije, isDeleted2, isActive, kvadratura, brSprata, brSobe, opremaLista);

            ProstorijaFileStorage.UpdateProstorija(selektovana.IDprostorije, p);

            //selektovana.OpremaLista.Remove(opremaZaIzmenu);
            //selektovana.OpremaLista.Add(o);
            parent.updateTable();
            this.Close();
        }
    }
}
