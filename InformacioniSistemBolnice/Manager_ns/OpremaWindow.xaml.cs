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
        private Room selektovana;
        public OpremaWindow(Room selektovana, WindowProstorije parent)
        {
            this.parent = parent;
            this.selektovana = selektovana;
            InitializeComponent();
            this.DataContext = this;
            if (selektovana.InventoryList != null)
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
                Inventory o = (Inventory)dataGridOprema.SelectedItem;
                //Oprema opremaZaIzmenu = OpremaFileStorage.GetOne(o.Sifra);
                Inventory opremaZaIzmenu = selektovana.GetOne(o.InventoryId);
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
                    Inventory selektovan = (Inventory)dataGridOprema.SelectedItem;
                    //OpremaFileStorage.RemoveOprema(selektovan.Sifra);
                    //selektovana.OpremaLista.Remove(selektovan);
                    dataGridOprema.Items.Remove(dataGridOprema.SelectedItem);

                    int idProstorije = selektovana.RoomId;
                    String naziv = selektovana.Name;
                    RoomType tipProstorije = selektovana.RoomType;
                    Boolean isDeleted = selektovana.IsDeleted;
                    Boolean isActive = selektovana.IsActive;
                    Double kvadratura = selektovana.Area;
                    int brSprata = selektovana.Floor;
                    int brSobe = selektovana.RoomNumber;
                    List<Inventory> opremaLista = selektovana.InventoryList;

                    foreach (Inventory op in opremaLista.ToList())
                    {
                        if (op.InventoryId.Equals(selektovan.InventoryId))
                        {
                            opremaLista.Remove(selektovan);
                        }
                    }
                    Room p = new Room(naziv, idProstorije, tipProstorije, isDeleted, isActive, kvadratura, brSprata, brSobe, opremaLista);
                    RoomFileRepository.UpdateRoom(idProstorije, p);
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
                Inventory o = (Inventory)dataGridOprema.SelectedItem;
                Inventory opremaZaPremestanje = selektovana.GetOne(o.InventoryId);
                if (o.InventoryType == InventoryType.dinamicInv)
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
            List<Inventory> opremaLista = selektovana.InventoryList;
            foreach (Inventory o in opremaLista)
            {
                if (!o.IsDeleted)
                    dataGridOprema.Items.Add(o);
            }
        }

        private void Pretraga_TextChanged(object sender, KeyEventArgs e)
        {
            dataGridOprema.Items.Clear();
            List<Inventory> opremaLista = selektovana.InventoryList;
            var filtered = opremaLista.Where(oprema => oprema.Name.StartsWith(Pretraga.Text) || oprema.Name.Contains(Pretraga.Text));

            dataGridOprema.ItemsSource = filtered;
        }
    }
}
