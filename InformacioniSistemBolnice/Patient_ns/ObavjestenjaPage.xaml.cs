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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ObavjestenjaPage.xaml
    /// </summary>
    public partial class ObavjestenjaPage : Page
    {

        private PocetnaPacijent parent;
        public ObavjestenjaPage(PocetnaPacijent pp)
        {
            parent = pp;
            InitializeComponent();
            updateVisibility();
            this.DataContext = this;
            LoadNotifications();
            LoadReminders();
        }
        private void LoadNotifications()
        {
            DateTime now = DateTime.Now;
            foreach (Therapy t in parent.Pacijent.zdravstveniKarton.Terapija)
            {
                if (t.BeginningDate < now && t.EndingDate > now)
                {

                    PrikazObavjestenja.Items.Add(t.Description);
                }
            }
        }

        private void LoadReminders()
        {
            List<Anamnesis> anamneses = AnamnesisFileRepository.GetAll();
            foreach (Anamnesis a in anamneses)
            {
                if (a.UsernameOfPatient.Equals(parent.Pacijent.korisnickoIme))
                {
                    foreach (Note n in a.NotesForAnamnesis)
                    {
                        if (n.IsSetReminder == true)
                        {
                            PrikazObavjestenja.Items.Add(n.DescriptionOfNote);
                        }
                    }
                }
            }
        }


        private void updateVisibility()
        {
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.imePacijenta.Visibility = Visibility.Visible;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new StartPacijentPage(parent);
        }
    }
}
