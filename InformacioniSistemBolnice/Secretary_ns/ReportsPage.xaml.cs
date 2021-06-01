using InformacioniSistemBolnice.Reports;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class ReportsPage : Page
    {
        private static ReportsPage _instance;
        private PrintDialog _printDialog = new PrintDialog();
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public static ReportsPage GetPage(SecretaryMain parent)
        {
            if (_instance == null)
                _instance = new ReportsPage();
            parent.Title.Content = "Izveštaj";
            return _instance;
        }
        private ReportsPage()
        {
            From = DateTime.Now.Date;
            To = DateTime.Now.Date;
            InitializeComponent();
            this.DataContext = this;
        }

        private void Generate1_Click(object sender, RoutedEventArgs e)
        {
            if (From == null || To == null)
            {
                MessageBox.Show("Unete vrednosti za datume su neodgovarajuće", "Neodgovarajuće vrednosti", MessageBoxButton.OK);
                return;
            }
            if (Report1DatePicker.SelectedDate.ToString().Length == 0)
            {
                MessageBox.Show("Prva uneta vrednost ne sme biti prazna", "Neodgovarajuća vrednost", MessageBoxButton.OK);
                return;
            }
            if (Report1DatePickerTo.SelectedDate.ToString().Length == 0)
            {
                MessageBox.Show("Druga uneta vrednost ne sme biti prazna", "Neodgovarajuća vrednost", MessageBoxButton.OK);
                return;
            }
            if (To < From)
            {
                MessageBox.Show("Unete vrednosti za datume su neodgovarajuće", "Neodgovarajuće vrednosti", MessageBoxButton.OK);
                return;
            }
            _printDialog.PrintVisual(new SecretaryReport1(From, To), "Izveštaj 1");
        }
    }
}
