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
        private Pacijent pacijent;
        public ObavjestenjaPage(PocetnaPacijent pp,Pacijent pac)
        {
            parent = pp;
            pacijent = pac;
            InitializeComponent();
            updateVisibility();
            this.DataContext = this;
            LoadNotifications();
            LoadReminders();
        }

        private void LoadReminders()
        {
            List<Anamnesis> anamneses = AnamnesisFileRepository.GetAll();
            foreach (Anamnesis a in anamneses )
            {
                if (a.UsernameOfPatient.Equals(pacijent.korisnickoIme))
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

        private void LoadNotifications()
        {
            DateTime now = DateTime.Now;
            foreach (Terapija t in pacijent.zdravstveniKarton.Terapija)
            {
                if (t.PocetakTerapije < now && t.KrajTerapije > now)
                {

                    PrikazObavjestenja.Items.Add(t.Opis);
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
