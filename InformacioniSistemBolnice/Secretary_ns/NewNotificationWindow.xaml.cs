using InformacioniSistemBolnice.User;
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
        private PatientController _patientController = new PatientController();
        public List<String> Recipients { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationContent { get; set; }
        public NewNotificationWindow(StartingPage parent)
        {
            this._parent = parent;
            InitializeComponent();
            // PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) Close(); };
            InitializeRecipients();
            this.DataContext = this;
        }

        

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipientsListBox.SelectedItems.Count == 0)
                return;
            int id = _notificationController.GetAll().Count;
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
            foreach (Patient patient in _patientController.GetAll())
            {
                if (!patient.IsDeleted)
                    Recipients.Add(patient.Name + " " + patient.Surname + " - " + patient.Username);
            }
            RecipientsListBox.SelectedIndex = 0;
        }

    }
}
