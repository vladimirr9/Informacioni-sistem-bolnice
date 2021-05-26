﻿using InformacioniSistemBolnice.Korisnik;
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
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class StartingPage : Page
    {
        public List<Notification> Notifications { get; set; }
        private Secretary _currentSecretary;
        private static StartingPage _instance;
        private StartingPage(Secretary currentSecretary)
        {
            this._currentSecretary = currentSecretary;
            
            InitializeComponent();
            UpdateTable();
        }

        public static StartingPage GetPage(Secretary currentSecretary)
        {
            if (_instance == null)
                _instance = new StartingPage(currentSecretary);
            else
                _instance.UpdateTable();
            return _instance;
        }

        

        private void NewNotification_Click(object sender, RoutedEventArgs e)
        {
            NewNotificationWindow window = new NewNotificationWindow(this);
            window.Show();
        }

        private void EditNotification_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationListView.SelectedItem != null && ((Notification)NotificationListView.SelectedItem).Recipients.Contains("ALL_USERS"))
            {
                Notification initialNotification = NotificationFileStorage.GetOne(((Notification)(NotificationListView.SelectedItem)).ID);
                EditNotificationWindow window = new EditNotificationWindow(this, initialNotification);
                window.Show();
            }
        }

        private void DeleteNotification_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationListView.SelectedItem == null)
                return;
            
            var result = MessageBox.Show("Da li ste sigurni da želite da obrišete ovo obavestenje?", "Potvrda brisanja", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;
            
            int id = ((Notification)NotificationListView.SelectedItem).ID;
            NotificationFileStorage.RemoveNotification(id);
            UpdateTable();
            
            
        }

        public void UpdateTable()
        {
            Notifications = new List<Notification>();
            foreach (Notification notification in NotificationFileStorage.GetAll())
            {
                if (notification.IsDirectedTo(_currentSecretary.korisnickoIme))
                {
                    if (!notification.IsDeleted)
                        Notifications.Add(notification);
                }
            }
            NotificationListView.ItemsSource = Notifications;
        }


        public class CustomMultiValueConvertor : IMultiValueConverter

        {
            public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return String.Concat(values[0], " ", values[1]);
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
            {
                return (value as string).Split(' ');
            }
        }
        
    }
}