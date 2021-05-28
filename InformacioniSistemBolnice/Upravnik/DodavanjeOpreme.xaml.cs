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
        private Room selektovana;
        public DodavanjeOpreme(Room p, OpremaWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            selektovana = p;

            IdProstorije.Text = selektovana.RoomId.ToString();
        }

        private void DodajOpremu(object sender, RoutedEventArgs e)
        {
            int idProstorije = selektovana.RoomId;
            String sifra = Sifra.Text;
            String naziv = Naziv.Text;
            InventoryType tipOpreme;
            if (TipOpreme.SelectedIndex == 0)
            {
                tipOpreme = 0;
            }
            else
            {
                tipOpreme = (InventoryType)1;
            }
            int kolicina = Convert.ToInt32(Kolicina.Text);
            Boolean isDeleted = (bool)IsDeleted.IsChecked;

            Inventory o = new Inventory(idProstorije, sifra, naziv, tipOpreme, kolicina, isDeleted);

            int idProstorije2 = selektovana.RoomId;
            String naziv2 = selektovana.Name;
            RoomType tipProstorije = selektovana.RoomType;
            Boolean isDeleted2 = selektovana.IsDeleted;
            Boolean isActive = selektovana.IsActive;
            Double kvadratura = selektovana.Area;
            int brSprata = selektovana.FloorNumber;
            int brSobe = selektovana.RoomNumber;
            List<Inventory> opremaLista = selektovana.InventoryList;

            opremaLista.Add(o);
            //Oprema o1 = new Oprema(sifra, Name, tipOpreme, kolicina, IsDeleted);
            //OpremaFileStorage.AddOprema(o)
            //;
            //selektovana.OpremaLista.Add(o);

            Room p = new Room(naziv2, idProstorije2, tipProstorije, isDeleted2, isActive, kvadratura, brSprata, brSobe, opremaLista);

            RoomFileRepoistory.UpdateRoom(selektovana.RoomId, p);
            parent.updateTable();
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
