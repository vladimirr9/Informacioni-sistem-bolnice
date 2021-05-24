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

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class NewNotificationWindow : Window
    {
        private StartingPage _parent;
        private List<String> _recipients;
        public NewNotificationWindow(StartingPage parent)
        {
            this._parent = parent;
            InitializeComponent();
            InitializeRecipients();
        }

        

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipientsListBox.SelectedItems.Count == 0)
                return;
            String title = TitleTextBox.Text;
            String content = ContentsTextBox.Text;
            DateTime dateAndTime = DateTime.Now;
            int id = NotificationFileStorage.GetAll().Count;
            Notification newNotification = new Notification(id, title, content, dateAndTime);
            FillRecipients(newNotification);
            NotificationFileStorage.AddNotification(newNotification);
            _parent.UpdateTable();
            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FillRecipients(Notification newNotification)
        {
            foreach (var item in RecipientsListBox.SelectedItems)
            {
                if (item.ToString().Equals("Svi korisnici"))
                    newNotification.Recipients.Add("ALL_USERS");
                else if (item.ToString().Equals("Zaposleni"))
                    newNotification.Recipients.Add("EMPLOYED_USERS");
                else if (item.ToString().Equals("Svi pacijenti"))
                    newNotification.Recipients.Add("PATIENT_USERS");
                else
                {
                    String username = item.ToString().Split('-')[1].Trim();
                    newNotification.Recipients.Add(username);
                }
            }
        }
        private void InitializeRecipients()
        {
            _recipients = new List<String>();
            _recipients.Add("Svi korisnici");
            _recipients.Add("Zaposleni");
            _recipients.Add("Svi pacijenti");
            foreach (Pacijent patient in PacijentFileStorage.GetAll())
            {
                if (!patient.isDeleted)
                    _recipients.Add(patient.ime + " " + patient.prezime + " - " + patient.korisnickoIme);
            }
            RecipientsListBox.ItemsSource = _recipients;
            RecipientsListBox.SelectedIndex = 0;
        }

        
    }
}
