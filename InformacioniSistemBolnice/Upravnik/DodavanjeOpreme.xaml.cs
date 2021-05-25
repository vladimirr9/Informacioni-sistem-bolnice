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
    /// Interaction logic for DodavanjeOpreme.xaml
    /// </summary>
    public partial class DodavanjeOpreme : Window
    {
        private OpremaWindow parent;
        private Prostorija selektovana;
        public DodavanjeOpreme(Prostorija p, OpremaWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            selektovana = p;

            IdProstorije.Text = selektovana.IDprostorije.ToString();
        }

        private void DodajOpremu(object sender, RoutedEventArgs e)
        {
            int idProstorije = selektovana.IDprostorije;
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

            opremaLista.Add(o);
            //Oprema o1 = new Oprema(sifra, Name, tipOpreme, kolicina, IsDeleted);
            //OpremaFileStorage.AddOprema(o)
            //;
            //selektovana.OpremaLista.Add(o);

            Prostorija p = new Prostorija(naziv2, idProstorije2, tipProstorije, isDeleted2, isActive, kvadratura, brSprata, brSobe, opremaLista);

            ProstorijaFileStorage.UpdateProstorija(selektovana.IDprostorije, p);
            parent.updateTable();
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
