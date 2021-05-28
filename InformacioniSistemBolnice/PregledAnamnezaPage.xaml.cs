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
    /// Interaction logic for PregledAnamnezaPage.xaml
    /// </summary>
    public partial class PregledAnamnezaPage : Page
    {
        private static PocetnaPacijent parent;
        public static PacijentKartonPage karton { get; set; }
        private static String UsernameOfLoggedInPatient;
        public static Anamnesis selectedAnamnesis { get; set; }
        public PregledAnamnezaPage(PocetnaPacijent pp, PacijentKartonPage pkp)
        {
            parent = pp;
            UsernameOfLoggedInPatient = pp.Pacijent.korisnickoIme;
            karton = pkp;
            InitializeComponent();
            this.DataContext = this;
            addNoteButton.IsEnabled = false;
            FillTable();
        }

        private void FillTable()
        {
            List<Anamnesis> anamneses = AnamnesisFileRepository.GetAll();
            foreach (Anamnesis a in anamneses)
            {
                if (a.UsernameOfPatient.Equals(UsernameOfLoggedInPatient))
                {
                    DataGridAnamneses.Items.Add(a);
                }
            }
        }

        private void addNoteButton_Click(object sender, RoutedEventArgs e)
        {
            selectedAnamnesis = (Anamnesis)DataGridAnamneses.SelectedItem;
            karton.borderWindow.Content = new AddingNotePage(parent, karton, selectedAnamnesis);
        }

        private void ShowNotesForAnamnesis(object sender, RoutedEventArgs e)
        {
            karton.borderWindow.Content = new ShowNotesPage(karton, selectedAnamnesis);
        }

        private void DataGridAnamneses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAnamnesis = (Anamnesis)DataGridAnamneses.SelectedItem;
            addNoteButton.IsEnabled = true;
        }
    }
}

