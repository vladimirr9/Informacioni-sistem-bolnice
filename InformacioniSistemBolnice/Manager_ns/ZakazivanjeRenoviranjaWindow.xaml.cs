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
    /// Interaction logic for ZakazivanjeRenoviranjaWindow.xaml
    /// </summary>
    public partial class ZakazivanjeRenoviranjaWindow : Window
    {
        private WindowProstorije _parent;
        private Room _selectedRoom;
        private RenovationPeriodController _renovationPeriodController = new RenovationPeriodController();
        public ZakazivanjeRenoviranjaWindow(Room room, WindowProstorije parent)
        {
            InitializeComponent();
            this._parent = parent;
            _selectedRoom = room;
            _parent.UpdateTable();
            CalendarSettings();
        }

        private void ZakaziRenoviranje(object sender, RoutedEventArgs e)
        {
            DateTime dateFrom = (DateTime)DateFrom.SelectedDate;
            DateTime dateTo = (DateTime)DateTo.SelectedDate;
            
            //if(_renovationPeriodController.IsAvailable(_selectedRoom, dateFrom, dateTo))
            //{
            _renovationPeriodController.ScheduleRenovation(_selectedRoom, dateFrom, dateTo);
            /*} 
            else
            {
                MessageBox.Show("Ima zakazanih termina u tom periodu!", "Upozorenje", MessageBoxButton.OK);
                this.Close();
            }*/
            _parent.UpdateTable();
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void CalendarSettings()
        {
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            DateFrom.BlackoutDates.Add(kalendar);
            DateTo.BlackoutDates.Add(kalendar);
        }

        /*public IEnumerable<DateTime> PeriodRenoviranja(DateTime from, DateTime to)
        {
            for (var dan = from.Date; dan.Date <= to.Date; dan = dan.AddDays(1))
                yield return dan;
        }*/

        //Room p = new Room(naziv2, idProstorije2, tipProstorije, isDeleted2, isActive, kvadratura, brSprata, brSobe, opremaLista);
        //RoomFileRepository.Update(_selectedRoom.RoomId, p);//int interval = 1;
        //CalendarDateRange periodRenoviranja = new CalendarDateRange(datumOd, datumDo);

        /*int idProstorije2 = _selectedRoom.RoomId;
        String naziv2 = _selectedRoom.Name;
        RoomType tipProstorije = _selectedRoom.RoomType;
        Boolean isDeleted2 = _selectedRoom.IsDeleted;
        Boolean isActive = _selectedRoom.IsActive;
        Double kvadratura = _selectedRoom.Area;
        int brSprata = _selectedRoom.Floor;
        int brSobe = _selectedRoom.RoomNumber;
        List<Inventory> opremaLista = _selectedRoom.InventoryList;

        List<Appointment> listaTermina = AppointmentFileRepository.GetAll();

        if (dateFrom < dateTo)
        {
            //foreach (DateTime day in PeriodRenoviranja(datumOd, datumDo))
            //{
                //foreach (Appointment t in listaTermina)
                //{
                    //if (t.AppointmentDate.Date != day)
            if(_selectedRoom.IsAvailable(dateFrom, dateTo))
            {
                if (DateTime.Today.Date == dateFrom.Date)
                {
                    isActive = false;
                }
                else
                {
                    isActive = true;
                }
            }
            else
            {
                MessageBox.Show("Ima zakazanih termina u tom periodu!", "Upozorenje", MessageBoxButton.OK);
                this.Close();
            }
                //}
           // }
        }
        else
        {
            MessageBox.Show("Uneli ste neispravan period!", "Upozorenje", MessageBoxButton.OK);
            this.Close();
        }*/
    }
}
