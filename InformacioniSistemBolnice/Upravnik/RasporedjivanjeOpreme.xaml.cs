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
    /// Interaction logic for RasporedjivanjeOpreme.xaml
    /// </summary>
    public partial class RasporedjivanjeOpreme : Window
    {
        private OpremaWindow parent;
        private Oprema selektovana;
        private Prostorija izabrana;
        public RasporedjivanjeOpreme(Prostorija p, Oprema o, OpremaWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            selektovana = o;
            izabrana = p;
            List<Prostorija> prostorijeSve = ProstorijaFileStorage.GetAll();
            List<Prostorija> prostorije = new List<Prostorija>();
            foreach (Prostorija pr in prostorijeSve)
            {
                if (!izabrana.Naziv.Equals(pr.Naziv))
                {
                    prostorije.Add(pr);
                }
            }
            Prostorija.ItemsSource = prostorije;
        }

        private void RasporediOpremu(object sender, RoutedEventArgs e)
        {
            Prostorija prostorija = (Prostorija)Prostorija.SelectedItem;
            int kolicina = Convert.ToInt32(Kolicina.Text);

            int idProstorije = prostorija.IDprostorije;
            String naziv = prostorija.Naziv;
            TipProstorije tipProstorije = prostorija.TipProstorije;
            Boolean isDeleted = prostorija.IsDeleted;
            Boolean isActive = prostorija.IsActive;
            Double kvadratura = prostorija.Kvadratura;
            int brSprata = prostorija.BrSprata;
            int brSobe = prostorija.BrSobe;
            List<Oprema> opremaLista1 = prostorija.OpremaLista;

            foreach (Oprema o in opremaLista1)
            {
                if (o.Sifra.Equals(selektovana.Sifra) && selektovana.Kolicina > kolicina)
                {
                    opremaLista1[opremaLista1.IndexOf(o)].Kolicina += kolicina;
                }
            }

            int idProstorije2 = izabrana.IDprostorije;
            String naziv2 = izabrana.Naziv;
            TipProstorije tipProstorije2 = izabrana.TipProstorije;
            Boolean isDeleted2 = izabrana.IsDeleted;
            Boolean isActive2 = izabrana.IsActive;
            Double kvadratura2 = izabrana.Kvadratura;
            int brSprata2 = izabrana.BrSprata;
            int brSobe2 = izabrana.BrSobe;
            List<Oprema> opremaLista2 = izabrana.OpremaLista;

            foreach (Oprema o in opremaLista2)
            {
                if (o.Sifra.Equals(selektovana.Sifra) && selektovana.Kolicina > kolicina)
                {
                    opremaLista2[opremaLista2.IndexOf(o)].Kolicina -= kolicina;
                }
            }

            Prostorija p1 = new Prostorija(naziv, idProstorije, tipProstorije, isDeleted, isActive, kvadratura, brSprata, brSobe, opremaLista1);
            //int novaKolicina = prostorija.GetOne(selektovana.Sifra).Kolicina + kolicina;
            //izabrana.GetOne(selektovana.Sifra).Kolicina -= kolicina;
            ProstorijaFileStorage.UpdateProstorija(prostorija.IDprostorije, p1);
            parent.updateTable();

            Prostorija p2 = new Prostorija(naziv2, idProstorije2, tipProstorije2, isDeleted2, isActive2, kvadratura2, brSprata2, brSobe2, opremaLista2);
            ProstorijaFileStorage.UpdateProstorija(izabrana.IDprostorije, p2);

            parent.updateTable();

            if (selektovana.Kolicina < kolicina)
            {
                MessageBoxResult odgovor = MessageBox.Show("Nema dovoljno opreme", "Greška", MessageBoxButton.OK);
                if (odgovor == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
