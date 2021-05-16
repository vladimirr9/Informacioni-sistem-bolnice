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

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class EditNotificationWindow : Window
    {
        private StartingPage _parent;
        private Notification initialNotification;
        private List<String> _recipients;
        public EditNotificationWindow(StartingPage parent, Notification initialNotification)
        {
            this._parent = parent;
            this.initialNotification = initialNotification;
            InitializeComponent();
            InitializeRecipients();

            TitleTextBox.Text = initialNotification.Title;
            ContentTextBox.Text = initialNotification.Content;

        }



        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            initialNotification.Title = TitleTextBox.Text;
            initialNotification.Content = ContentTextBox.Text;
            initialNotification.CreationDate = DateTime.Now;

            NotificationFileStorage.UpdateNotification(initialNotification.ID, initialNotification);
            _parent.UpdateTable();
            Close();
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
            Primalac.ItemsSource = _recipients;
            Primalac.SelectedIndex = 0;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
