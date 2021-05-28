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
        private Inventory opremaZaIzmenu;
        private Room selektovana;
        public IzmenaOpreme(Room p, Inventory o, OpremaWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            opremaZaIzmenu = o;
            selektovana = p;

            IdProstorije.Text = selektovana.GetOne(opremaZaIzmenu.Id).RoomId.ToString();
            Sifra.Text = opremaZaIzmenu.Id;
            Naziv.Text = opremaZaIzmenu.Name;
            if (opremaZaIzmenu.InventoryType == 0)
            {
                TipOpreme.SelectedIndex = 0;
            }
            else
            {
                TipOpreme.SelectedIndex = 1;
            }
            Kolicina.Text = opremaZaIzmenu.Quantity.ToString();
            IsDeleted.IsChecked = opremaZaIzmenu.IsDeleted;
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IzmeniOpremu(object sender, RoutedEventArgs e)
        {
            int idProstorije = selektovana.GetOne(opremaZaIzmenu.Id).RoomId;
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

            opremaLista.Remove(opremaZaIzmenu);
            opremaLista.Add(o);

            //Oprema o = new Oprema(sifra, Name, tipOpreme, kolicina, IsDeleted);
            //OpremaFileStorage.UpdateOprema(opremaZaIzmenu.Sifra, o);
            Room p = new Room(naziv2, idProstorije2, tipProstorije, isDeleted2, isActive, kvadratura, brSprata, brSobe, opremaLista);

            RoomFileRepoistory.UpdateRoom(selektovana.RoomId, p);

            //selektovana.OpremaLista.Remove(opremaZaIzmenu);
            //selektovana.OpremaLista.Add(o);
            parent.updateTable();
            this.Close();
        }
    }
}
