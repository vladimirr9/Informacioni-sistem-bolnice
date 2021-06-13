using InformacioniSistemBolnice.User;
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
using InformacioniSistemBolnice.Controller;

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class StartingPage : Page
    {
        public List<Notification> Notifications { get; set; }
        private Secretary _currentSecretary;
        private static StartingPage _instance;
        private NotificationController _notificationController = new NotificationController();
        private SecretaryMain _parent;
        private StartingPage(Secretary currentSecretary, SecretaryMain parent)
        {
            this._parent = parent;
            this._currentSecretary = currentSecretary;
            
            InitializeComponent();
            this.DataContext = this;
            UpdateTable();
        }

        public static StartingPage GetPage(Secretary currentSecretary, SecretaryMain parent)
        {
            if (_instance == null)
                _instance = new StartingPage(currentSecretary, parent);
            else
                _instance.UpdateTable();
            parent.Title.Content = "Početna";
            return _instance;
        }

        

        private void NewNotification_Click(object sender, RoutedEventArgs e)
        {
            NewNotification();
        }
        private void NewNotification()
        {
            NewNotificationWindow window = new NewNotificationWindow(this);
            window.Show();
        }

        private void EditNotification_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationListView.SelectedItem != null && ((Notification)NotificationListView.SelectedItem).Recipients.Contains("ALL_USERS"))
            {
                Notification initialNotification = _notificationController.GetOne(((Notification)NotificationListView.SelectedItem).ID);
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

            Notification notification = (Notification)NotificationListView.SelectedItem;
            _notificationController.Delete(notification);
            UpdateTable();
            
            
        }

        public void UpdateTable()
        {
            Notifications = new List<Notification>();
            foreach (Notification notification in _notificationController.GetAll())
            {
                if (notification.IsDirectedTo(_currentSecretary.Username))
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

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _parent.Main.Content == this;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NewNotification();
        }
    }
}
