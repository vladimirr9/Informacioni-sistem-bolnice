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
    /// Interaction logic for UpravnikWindow.xaml
    /// </summary>
    public partial class UpravnikWindow : Window
    {
        private Manager _loggedManager;
        public UpravnikWindow(Manager manager)
        {
            InitializeComponent();
            _loggedManager = manager;
            FillLabels();
        }

        private void WindowProstorije(object sender, RoutedEventArgs e)
        {
            WindowProstorije wp = new WindowProstorije(this);
            wp.Show();
        }

        private void LekoviWindow(object sender, RoutedEventArgs e)
        {
            LekoviWindow lw = new LekoviWindow(this);
            lw.Show();
        }

        private void Pocetna(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            this.Close();
        }

        private void FillLabels()
        {
            NameLabel.Content = _loggedManager.Name + " " + _loggedManager.Surname;
            DateLabel.Content = _loggedManager.DateOfBirth.Date;
            AddressLabel.Content = _loggedManager.ResidentialAddress.StreetAndNumber + " " + _loggedManager.ResidentialAddress.City;
            JMBGLabel.Content = _loggedManager.JMBG;
            EmailLabel.Content = _loggedManager.Email;
            NumberLabel.Content = _loggedManager.PhoneNumber;
        }
    }
}
