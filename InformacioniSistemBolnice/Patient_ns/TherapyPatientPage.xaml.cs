using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for TherapyPatientPage.xaml
    /// </summary>
    public partial class TherapyPatientPage : Page
    {
        private StartPatientWindow _parentp;
        private Patient _patient;
        private PrintDialog _printDialog = new PrintDialog();

        public TherapyPatientPage(StartPatientWindow spw)
        {
            _parentp = spw;
            _patient = spw.Patient;
            InitializeComponent();
            calendarComponents();

            mjeseci.SelectionChanged += (o, e) => osvjeziPrikazKalendara();
            godine.SelectionChanged += (o, e) => osvjeziPrikazKalendara();


        }

        private void dodajNotes()
        {
            string str;
            foreach (Prescription p in _patient.MedicalRecord.recept)
            {
                str = "Lek: " + p.Drug.Name + Environment.NewLine + "Vreme: " + p.Date.ToShortTimeString();
                

                string boo = p.Date.ToUniversalTime().ToString("MMMM");
                int th = DateTime.ParseExact(boo, "MMMM", System.Globalization.CultureInfo.CurrentCulture).Month;
                if (mjeseci.SelectedIndex == (th - 1))

                {
                    Calendar.Days[(int) p.Date.Day + 1].Notes = str;
                   
                }

            }
        }

        private void osvjeziPrikazKalendara()
        {
            if (godine.SelectedItem == null) return;
            if (mjeseci.SelectedItem == null) return;

            int year = (int) godine.SelectedItem;

            int month = mjeseci.SelectedIndex + 1;

            DateTime targetDate = new DateTime(year, month, 1);

            Calendar.BuildCalendar(targetDate);

            dodajNotes();

        }

        public void calendarComponents()
        {
            List<string> mjesec = new List<string>
            {
                "Januar", "Februar", "Mart", "April", "Maj", "Jun", "Jul", "Avgust",
                "Septembar", "Oktobar", "Novembar", "Decembar"
            };
            foreach (String m in mjesec)
            {
                mjeseci.Items.Add(m);
            }

            for (int i = -50; i < 50; i++)
            {
                godine.Items.Add(DateTime.Today.AddYears(i).Year);
            }

            godine.SelectedItem = DateTime.Today.Year;

            string startMonth = DateTime.Now.ToUniversalTime().ToString("MMMM");
            int newmnths = DateTime.ParseExact(startMonth, "MMMM", System.Globalization.CultureInfo.CurrentCulture)
                .Month;
            mjeseci.SelectedIndex = newmnths - 1;

            dodajNotes();

            //double width = 0;
            //foreach(ComboBoxItem item in mjeseci.Items)
            //{
            //    item.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //    if (item.DesiredSize.Width > width)
            //        width = item.DesiredSize.Width;
            //}
            //mjeseci.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            generateReport.Visibility = Visibility.Hidden;
            _printDialog.PrintVisual(this,"Kalendar-izveštaj");
            generateReport.Visibility = Visibility.Visible;

        }
    }
}
