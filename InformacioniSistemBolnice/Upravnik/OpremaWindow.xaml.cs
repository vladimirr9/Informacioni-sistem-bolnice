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
    /// Interaction logic for OpremaWindow.xaml
    /// </summary>
    public partial class OpremaWindow : Window
    {
        private WindowProstorije parent;
        private Prostorija selektovana;
        public OpremaWindow(Prostorija selektovana, WindowProstorije parent)
        {
            this.parent = parent;
            this.selektovana = selektovana;
            InitializeComponent();
            this.DataContext = this;
            if (selektovana.OpremaLista != null)
            {
                updateTable();
            }
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajNovuOpremu(object sender, RoutedEventArgs e)
        {
            DodavanjeOpreme prozor = new DodavanjeOpreme(selektovana, this);
            prozor.Show();
        }

        private void IzmeniOpremu(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedItem != null)
            {
                Oprema o = (Oprema)dataGridOprema.SelectedItem;
                //Oprema opremaZaIzmenu = OpremaFileStorage.GetOne(o.Sifra);
                Oprema opremaZaIzmenu = selektovana.GetOne(o.Sifra);
                IzmenaOpreme prozor = new IzmenaOpreme(selektovana, opremaZaIzmenu, this);
                prozor.Show();
            }
        }

        private void ObrisiOpremu(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedItem != null)
            {
                MessageBoxResult odgovor = MessageBox.Show("Da li želite da obrišete selektovanu opremu?", "Potvrda brisanja opreme", MessageBoxButton.YesNo);
                if (odgovor == MessageBoxResult.Yes)
                {
                    Oprema selektovan = (Oprema)dataGridOprema.SelectedItem;
                    //OpremaFileStorage.RemoveOprema(selektovan.Sifra);
                    //selektovana.OpremaLista.Remove(selektovan);
                    dataGridOprema.Items.Remove(dataGridOprema.SelectedItem);

                    int idProstorije = selektovana.IDprostorije;
                    String naziv = selektovana.Naziv;
                    TipProstorije tipProstorije = selektovana.TipProstorije;
                    Boolean isDeleted = selektovana.IsDeleted;
                    Boolean isActive = selektovana.IsActive;
                    Double kvadratura = selektovana.Kvadratura;
                    int brSprata = selektovana.BrSprata;
                    int brSobe = selektovana.BrSobe;
                    List<Oprema> opremaLista = selektovana.OpremaLista;

                    foreach (Oprema op in opremaLista.ToList())
                    {
                        if (op.Sifra.Equals(selektovan.Sifra))
                        {
                            opremaLista.Remove(selektovan);
                        }
                    }
                    Prostorija p = new Prostorija(naziv, idProstorije, tipProstorije, isDeleted, isActive, kvadratura, brSprata, brSobe, opremaLista);
                    ProstorijaFileStorage.UpdateProstorija(idProstorije, p);
                    //List<Oprema> oprema = selektovana.OpremaLista;
                    //oprema[oprema.IndexOf(selektovan)].IsDeleted = true;
                    updateTable();
                }
            }
        }

        private void RasporediOpremu(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedItem != null)
            {
                Oprema o = (Oprema)dataGridOprema.SelectedItem;
                Oprema opremaZaPremestanje = selektovana.GetOne(o.Sifra);
                if (o.TipOpreme == TipOpreme.dinamicka)
                {
                    RasporedjivanjeOpreme prozor = new RasporedjivanjeOpreme(selektovana, opremaZaPremestanje, this);
                    prozor.Show();
                }
                else
                {
                    RasporedjivanjeStatickeWindow prozor = new RasporedjivanjeStatickeWindow(selektovana, opremaZaPremestanje, this);
                    prozor.Show();
                }
            }
        }

        public void updateTable()
        {
            dataGridOprema.Items.Clear();
            //List<Oprema> oprema = OpremaFileStorage.GetAll();
            List<Oprema> opremaLista = selektovana.OpremaLista;
            foreach (Oprema o in opremaLista)
            {
                if (!o.IsDeleted)
                    dataGridOprema.Items.Add(o);
            }
        }

        private void Pretraga_TextChanged(object sender, KeyEventArgs e)
        {
            dataGridOprema.Items.Clear();
            List<Oprema> opremaLista = selektovana.OpremaLista;
            var filtered = opremaLista.Where(oprema => oprema.Naziv.StartsWith(Pretraga.Text) || oprema.Naziv.Contains(Pretraga.Text));

            dataGridOprema.ItemsSource = filtered;
        }
    }
}
