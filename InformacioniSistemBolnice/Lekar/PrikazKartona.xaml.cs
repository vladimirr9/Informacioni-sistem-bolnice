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

namespace InformacioniSistemBolnice.Lekar
{
    /// <summary>
    /// Interaction logic for PrikazKartona.xaml
    /// </summary>
    public partial class PrikazKartona : Window
    {
        private LekarWindow parent;
        private Pacijent selektovan;
        private Termin termin;
        public PrikazKartona(Termin termin, LekarWindow parent)
        {
            this.selektovan = termin.Pacijent;
            this.parent = parent;
            this.termin = termin;
            InitializeComponent();
            FillKarton();
        }

        private void FillKarton()
        {
            Ime.Text = selektovan.ime + " " + selektovan.prezime;
            Datum.Text = selektovan.datumRodenja.ToString("dd/MM/yyyy");
            JMBG.Text = selektovan.jmbg;
            Zdravstvena.Text = selektovan.brojZdravstveneKartice;

            if (selektovan.pol == 'M')
            {
                Pol.SelectedIndex = 0;
            }
            else
            {
                Pol.SelectedItem = 1;
            }

            Anamneza.Document.Blocks.Clear();
            Anamneza.Document.Blocks.Add(new Paragraph(new Run(termin.anamneza)));
        }

        //izadvanje recepta i terapije
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String lek = Lek.Text;
            if (lek != "" && Pocetak.Text != "")
            {
                Recept recept = new Recept(lek, DateTime.Parse(Pocetak.Text), parent.lekar);
                selektovan.zdravstveniKarton.AddRecept(recept);
                PacijentFileStorage.UpdatePacijent(selektovan.korisnickoIme, selektovan);
                Lek.Text = "";
                Opis.Document.Blocks.Clear();
                Pocetak.SelectedDate = null;
                Kraj.SelectedDate = null;
                Ucestalost.Text = "";
            }
        }

        //Upisivanje anamneze
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String anamneza = new TextRange(Anamneza.Document.ContentStart, Anamneza.Document.ContentEnd).Text;
            if (anamneza.Trim() != "")
            {
                termin.anamneza = anamneza;


                int idOfAnamnesis = AnamnesisFileRepository.GetAll().Count + 1;
                Anamnesis newAnamnesis = new Anamnesis(anamneza, null, selektovan.korisnickoIme, idOfAnamnesis, DateTime.Now, termin.iDTermina);
                AnamnesisFileRepository.AddAnamnesis(newAnamnesis);// - dodati u prikaz kartona



                TerminFileStorage.UpdateTermin(termin.iDTermina, termin);
            }
        }

        //Izdavanje uputa
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LekarDodajTerminWindow dodajWindow = new LekarDodajTerminWindow(parent);
            Application.Current.MainWindow = dodajWindow;
            dodajWindow.Show();
        }
    }
}
