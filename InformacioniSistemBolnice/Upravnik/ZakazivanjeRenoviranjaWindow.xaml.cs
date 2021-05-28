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
    /// Interaction logic for ZakazivanjeRenoviranjaWindow.xaml
    /// </summary>
    public partial class ZakazivanjeRenoviranjaWindow : Window
    {
        private WindowProstorije parent;
        private Room selected;
        public ZakazivanjeRenoviranjaWindow(Room p, WindowProstorije parent)
        {
            InitializeComponent();
            this.parent = parent;
            selected = p;
            parent.updateTable();
            CalendarDateRange calendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            DateFrom.BlackoutDates.Add(calendar);
            DateTo.BlackoutDates.Add(calendar);
        }

        private void ZakaziRenoviranje(object sender, RoutedEventArgs e)
        {
            DateTime dateFrom = (DateTime)DateFrom.SelectedDate;
            DateTime dateTo = (DateTime)DateTo.SelectedDate;
            //int interval = 1;
            //CalendarDateRange periodRenoviranja = new CalendarDateRange(datumOd, datumDo);

            /*int idProstorije2 = selected.IDprostorije;
            String naziv2 = selected.Naziv;
            TipProstorije tipProstorije = selected.TipProstorije;
            Boolean isDeleted2 = selected.IsDeleted;
            Double kvadratura = selected.Kvadratura;
            int brSprata = selected.BrSprata;
            int brSobe = selected.BrSobe;
            List<Oprema> opremaLista = selected.OpremaLista;*/

            Boolean isActive = selected.IsActive;
            Boolean isDeleted = false;

            if (dateFrom < dateTo)
            {
                if (selected.IsAvailable(dateFrom, dateTo))
                {
                    foreach (DateTime day in RenPeriod(dateFrom, dateTo))
                    {
                        isActive = false;
                    }
                }
                else
                { 
                    MessageBox.Show("Ima zakazanih termina u tom periodu!", "Upozorenje", MessageBoxButton.OK);
                    this.Close();
                }    
            }
            else
            {
                MessageBox.Show("Uneli ste neispravan period!", "Upozorenje", MessageBoxButton.OK);
                this.Close();
            }

            Room p = new Room(selected.Name, selected.RoomId, selected.RoomType, selected.IsDeleted, isActive, selected.Area, selected.FloorNumber, selected.RoomNumber, selected.InventoryList);
            RenovationPeriod rp = new RenovationPeriod(dateFrom, dateTo, isDeleted, p);
            RoomFileRepoistory.UpdateRoom(selected.RoomId, p);
            RenovationPeriodFileStorage.AddRenovationPeriod(rp);
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public IEnumerable<DateTime> RenPeriod(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
