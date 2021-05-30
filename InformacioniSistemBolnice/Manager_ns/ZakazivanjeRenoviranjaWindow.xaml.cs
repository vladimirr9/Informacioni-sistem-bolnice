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
        private Room selektovana;
        public ZakazivanjeRenoviranjaWindow(Room p, WindowProstorije parent)
        {
            InitializeComponent();
            this.parent = parent;
            selektovana = p;
            parent.updateTable();
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            DatumOd.BlackoutDates.Add(kalendar);
            DatumDo.BlackoutDates.Add(kalendar);
        }

        private void ZakaziRenoviranje(object sender, RoutedEventArgs e)
        {
            DateTime datumOd = (DateTime)DatumOd.SelectedDate;
            DateTime datumDo = (DateTime)DatumDo.SelectedDate;
            int interval = 1;
            //CalendarDateRange periodRenoviranja = new CalendarDateRange(datumOd, datumDo);

            int idProstorije2 = selektovana.RoomId;
            String naziv2 = selektovana.Name;
            RoomType tipProstorije = selektovana.RoomType;
            Boolean isDeleted2 = selektovana.IsDeleted;
            Boolean isActive = selektovana.IsActive;
            Double kvadratura = selektovana.Area;
            int brSprata = selektovana.Floor;
            int brSobe = selektovana.RoomNumber;
            List<Inventory> opremaLista = selektovana.InventoryList;

            List<Appointment> listaTermina = AppointmentFileRepository.GetAll();

            if (datumOd < datumDo)
            {
                //foreach (DateTime day in PeriodRenoviranja(datumOd, datumDo))
                //{
                    //foreach (Appointment t in listaTermina)
                    //{
                        //if (t.AppointmentDate.Date != day)
                if(selektovana.IsAvailable(datumOd, datumDo))
                {
                    if (DateTime.Today.Date == datumOd.Date)
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
            }

            Room p = new Room(naziv2, idProstorije2, tipProstorije, isDeleted2, isActive, kvadratura, brSprata, brSobe, opremaLista);
            RoomFileRepository.UpdateRoom(selektovana.RoomId, p);
            parent.updateTable();
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*public IEnumerable<DateTime> PeriodRenoviranja(DateTime from, DateTime to)
        {
            for (var dan = from.Date; dan.Date <= to.Date; dan = dan.AddDays(1))
                yield return dan;
        }*/
    }
}
