using InformacioniSistemBolnice.Korisnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    public partial class NewNotificationWindow : Window
    {
        private StartingPage _parent;
        private NotificationController _notificationController = new NotificationController();
        public List<String> Recipients { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationContent { get; set; }
        public NewNotificationWindow(StartingPage parent)
        {
            this._parent = parent;
            InitializeComponent();
            InitializeRecipients();
            this.DataContext = this;
        }

        

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipientsListBox.SelectedItems.Count == 0)
                return;
            int id = NotificationFileStorage.GetAll().Count;
            Notification newNotification = new Notification(id, NotificationTitle, NotificationContent, DateTime.Now);
            List<string> selectedRecipients = RecipientsListBox.SelectedItems.Cast<string>().ToList();
            newNotification.FillRecipients(selectedRecipients);
            _notificationController.Create(newNotification);
            _parent.UpdateTable();
            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
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

        
    }
}
