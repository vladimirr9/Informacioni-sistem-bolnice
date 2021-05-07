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
using System.Windows.Shapes;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PopunjavanjeAnkete.xaml
    /// </summary>
    public partial class PopunjavanjeAnkete : Window
    {

        private Ocjenjivanje parentp;
        private Termin selektovan;
        public PopunjavanjeAnkete(Ocjenjivanje p,Termin t)
        {
            parentp = p;
            selektovan = t;
            InitializeComponent();
            submit.IsEnabled = false;
            if (parentp.kojiJePritisnut == parentp.rate)
            {
                imeLjekara.Text = "dr. " + t.Lekar.ime + " " + t.Lekar.prezime;
            }
            else {
                imeLjekara.Text = null;
            }
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            for (int i = 1; i < 6; i++) {
                rateComboBox.Items.Add(i);
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Ocjenjivanje o = new Ocjenjivanje(parentp.Parentp);
            Application.Current.MainWindow = o;
            o.Show();
            this.Close();
            return;
        }

        private void commentText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProvjeritiPopunjenostPolja();
        }

        private void ProvjeritiPopunjenostPolja()
        {
            if (commentText.Text != null && rateComboBox.SelectedItem != null) {
                submit.IsEnabled=true;
            }
        }

        private void rateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProvjeritiPopunjenostPolja();
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (parentp.kojiJePritisnut == parentp.rate)
            {
                int IdAnkete = AnketaFileStorage.GetAll().Count + 1;
                string komentar = commentText.Text;
                Anketa novaAnketa = new Anketa(IdAnkete, komentar, (int)rateComboBox.SelectedItem, selektovan.Lekar.korisnickoIme, selektovan.Pacijent.korisnickoIme, selektovan.iDTermina, false, DateTime.Now);
                AnketaFileStorage.AddAnketa(novaAnketa);
                this.Close();
                parentp.UpdateTable();
            }
            else {
                int IdAnkete = AnketaFileStorage.GetAll().Count + 1;
                string komentar = commentText.Text;
                Anketa novaAnketa = new Anketa(IdAnkete, komentar, (int)rateComboBox.SelectedItem, null, parentp.Parentp.Pacijent.korisnickoIme, 0, false, DateTime.Now);
                AnketaFileStorage.AddAnketa(novaAnketa);
                this.Close();
                parentp.rateHospital.Visibility = Visibility.Hidden;

            }

        }
    }
}
