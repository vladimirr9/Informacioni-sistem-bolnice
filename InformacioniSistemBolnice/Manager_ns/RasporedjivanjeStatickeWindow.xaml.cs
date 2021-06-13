using InformacioniSistemBolnice.Controller;
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
    /// Interaction logic for RasporedjivanjeStatickeWindow.xaml
    /// </summary>
    public partial class RasporedjivanjeStatickeWindow : Window
    {
        private OpremaWindow _parent;
        private Inventory _selectedinventory;
        private Room _selectedRoom;
        private RoomController _roomController = new RoomController();
        public RasporedjivanjeStatickeWindow(Room room, Inventory inventory, OpremaWindow parent)
        {
            InitializeComponent();
            this._parent = parent;
            _selectedinventory = inventory;
            _selectedRoom = room;
            RoomsComboBox.ItemsSource = _roomController.DisplayRoomsForRelocating(_selectedRoom);

            CalendarDateRange calendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            DateTo.BlackoutDates.Add(calendar);
        }

        private void RelocateInventory(object sender, RoutedEventArgs e)
        {
            Room destinationRoom = (Room)RoomsComboBox.SelectedItem;
            int quantity = Convert.ToInt32(Kolicina.Text);
            DateTime pickedDate = (DateTime)DateTo.SelectedDate;
            _roomController.StaticInventoryRelocation(destinationRoom, _selectedinventory, quantity, pickedDate);
            _parent.UpdateTable();
            this.Close();          
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} /*foreach (Oprema o in opremaLista1)
            {
                if (o.Sifra.Equals(selektovana.Sifra) && selektovana.Kolicina > kolicina)
                {
                    opremaLista1[opremaLista1.IndexOf(o)].Kolicina += kolicina;
                }
            }*/

/*int idProstorije2 = izabrana.RoomId;
String naziv2 = izabrana.Name;
RoomType tipProstorije2 = izabrana.RoomType;
Boolean isDeleted2 = izabrana.IsDeleted;
Boolean isActive2 = izabrana.IsActive;
Double kvadratura2 = izabrana.Area;
int brSprata2 = izabrana.Floor;
int brSobe2 = izabrana.RoomNumber;
List<Inventory> opremaLista2 = izabrana.InventoryList;

//DateTime start = DateTime.Today;
DateTime datumDo = (DateTime)DatumDo.SelectedDate;

if (DateTime.Today > datumDo)
{
    foreach (Inventory o in opremaLista2)
    {
        if (o.InventoryId.Equals(selektovana.InventoryId) && selektovana.Quantity > kolicina)
        {
            opremaLista2[opremaLista2.IndexOf(o)].Quantity -= kolicina;
            opremaLista1[opremaLista1.IndexOf(o)].Quantity += kolicina;
        }
    }
}

Room p1 = new Room(naziv, idProstorije, tipProstorije, isDeleted, isActive, kvadratura, brSprata, brSobe, opremaLista1);
//int novaKolicina = RoomComboBox.GetOne(selektovana.Sifra).Kolicina + kolicina;
//izabrana.GetOne(selektovana.Sifra).Kolicina -= kolicina;
RoomFileRepository.Update(prostorija.RoomId, p1);
//_parent.UpdateTable();

Room p2 = new Room(naziv2, idProstorije2, tipProstorije2, isDeleted2, isActive2, kvadratura2, brSprata2, brSobe2, opremaLista2);

RoomFileRepository.Update(prostorija.RoomId, p1);
RoomFileRepository.Update(izabrana.RoomId, p2);

parent.UpdateTable();

if (selektovana.Quantity < kolicina)
{
    MessageBoxResult odgovor = MessageBox.Show("Nema dovoljno opreme", "Greška", MessageBoxButton.OK);
    if (odgovor == MessageBoxResult.OK)
    {
        this.Close();
    }
}
this.Close();*/
