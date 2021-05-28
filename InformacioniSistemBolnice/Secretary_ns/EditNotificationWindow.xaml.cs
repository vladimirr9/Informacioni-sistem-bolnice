using InformacioniSistemBolnice.Korisnik;
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
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.Controller;

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class EditNotificationWindow : Window
    {
        private StartingPage _parent;
        private Notification initialNotification;
        private NotificationController _notificationController = new NotificationController();
        public List<String> Recipients { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationContent { get; set; }
        public EditNotificationWindow(StartingPage parent, Notification initialNotification)
        {
            this._parent = parent;
            
            this.initialNotification = initialNotification;
            InitializeComponent();
            this.DataContext = this;
            InitializeRecipients();

            NotificationTitle = initialNotification.Title;
            NotificationContent = initialNotification.Content;

        }



        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            initialNotification.Title = NotificationTitle;
            initialNotification.Content = NotificationContent;
            initialNotification.CreationDate = DateTime.Now;

            _notificationController.Update(initialNotification.ID, initialNotification);
            NotificationFileStorage.UpdateNotification(initialNotification.ID, initialNotification);
            _parent.UpdateTable();
            Close();
        }

        private void InitializeRecipients()
        {
            Recipients = new List<String>();
            Recipients.Add("Svi korisnici");
            Recipients.Add("Zaposleni");
            Recipients.Add("Svi pacijenti");
            foreach (Pacijent patient in PacijentFileStorage.GetAll())
            {
                if (!patient.isDeleted)
                    Recipients.Add(patient.ime + " " + patient.prezime + " - " + patient.korisnickoIme);
            }
            RecipientsListBox.SelectedIndex = 0;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
