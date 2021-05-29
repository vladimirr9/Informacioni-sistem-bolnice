using InformacioniSistemBolnice.FileStorage;
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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PopunjavanjeAnketePage.xaml
    /// </summary>
    public partial class PopunjavanjeAnketePage : Page
    {
        private PocetnaPacijent parent;
        private Termin selektovan;
        private AnketaPage parentp;
        public PopunjavanjeAnketePage(AnketaPage ap,PocetnaPacijent pp,Termin t)
        {
            parentp = ap;
            selektovan = t;
            parent = pp;
            InitializeComponent();
            submit.IsEnabled = false;
            if (parentp.kojiJePritisnut == parentp.rate)
            {
                parent.imeLjekara.Content = "dr. " + t.Doctor.ime + " " + t.Doctor.prezime;
            }
            else
            {
               parent.imeLjekara.Content = null;
            }
            InitializeComboBox();
            parent.UpdateVisibilityOfComponents();
        }

        

        private void InitializeComboBox()
        {
            for (int i = 1; i < 6; i++)
            {
                rateComboBox.Items.Add(i);
            }
        }

        private void ProvjeritiPopunjenostPolja()
        {
            if (commentText.Text != null && rateComboBox.SelectedItem != null)
            {
                submit.IsEnabled = true;
            }
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            updateVisibility();
            parent.startWindow.Content = new AnketaPage(parent);

        }

        private void rateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProvjeritiPopunjenostPolja();
        }

        private void commentText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProvjeritiPopunjenostPolja();
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (parentp.kojiJePritisnut == parentp.rate)
            {
                int IdAnkete = AnketaFileStorage.GetAll().Count + 1;
                string komentar = commentText.Text;
                Anketa novaAnketa = new Anketa(IdAnkete, komentar, (int)rateComboBox.SelectedItem, selektovan.Doctor.korisnickoIme, selektovan.Pacijent.korisnickoIme, selektovan.iDTermina, false, DateTime.Now);
                AnketaFileStorage.AddAnketa(novaAnketa);
                AnketaPage ap = new AnketaPage(parent);
                parentp.UpdateTable();
                parent.startWindow.Content = new AnketaPage(parent);
                parent.imePacijenta.Visibility = Visibility.Visible;
                parent.odjava.Visibility = Visibility.Visible;
            }
            else
            {
                int IdAnkete = AnketaFileStorage.GetAll().Count + 1;
                string komentar = commentText.Text;
                Anketa novaAnketa = new Anketa(IdAnkete, komentar, (int)rateComboBox.SelectedItem, null, parent.Pacijent.korisnickoIme, 0, false, DateTime.Now);
                AnketaFileStorage.AddAnketa(novaAnketa);
                AnketaPage ap = new AnketaPage(parent);
                parentp.rateHospital.Visibility = Visibility.Hidden;
                parent.startWindow.Content = new AnketaPage(parent);
                parent.imePacijenta.Visibility = Visibility.Visible;
                parent.odjava.Visibility = Visibility.Visible;

            }
        }

        private void updateVisibility()
        {
            parent.imeLjekara.Visibility = Visibility.Hidden;
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.imePacijenta.Visibility = Visibility.Visible;
        }
    }
}
