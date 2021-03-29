﻿using System;
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
    /// Interaction logic for LekarIzmeniTerminWindow.xaml
    /// </summary>
    public partial class LekarIzmeniTerminWindow : Window
    {
        private LekarWindow parent;
        private Termin selektovan;
        public LekarIzmeniTerminWindow(Termin selektovan, LekarWindow parent)
        {
            this.selektovan = selektovan;
            this.parent = parent;
            InitializeComponent();

            List<global::Lekar> lekari = LekarFileStorage.GetAll();
            lekar.ItemsSource = lekari;
            List<Pacijent> pacijenti = PacijentFileStorage.GetAll();
            pacijent.ItemsSource = pacijenti;
            //List<Prostorija> prostorije = ProstorijaFileStorage.GetAll();
            //prostorija.ItemsSource = prostorije;

            date.SelectedDate = selektovan.datumZakazivanja;
            time.SelectedValue = selektovan.datumZakazivanja.ToString("HH:mm");

            foreach(global::Lekar l in lekari)
            {
                if (l.jmbg == selektovan.Lekar.jmbg)
                    lekar.SelectedItem = l;
            }

            foreach (Pacijent p in pacijenti)
            {
                if (p.jmbg != null || p.jmbg == selektovan.pacijent.jmbg)
                    pacijent.SelectedItem = p;
            }

            tip.SelectedIndex = (int)selektovan.tipTermina;
        }
        //odustani
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //potvrdi
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Pacijent p = (Pacijent)pacijent.SelectedItem;
            global::Lekar l = (global::Lekar)lekar.SelectedItem;
            if (time.SelectedIndex != -1)
            {
                ComboBoxItem item = time.SelectedItem as ComboBoxItem;
                String t = item.Content.ToString();
                String d = date.Text;
                DateTime dt = DateTime.Parse(d + " " + t);
                TipTermina tt = (TipTermina)tip.SelectedIndex;
                Termin termin = new Termin(selektovan.iDTermina, dt, 15, tt, StatusTermina.zakazan, p, l);
                TerminFileStorage.UpdateTermin(selektovan.iDTermina, termin);
                parent.updateTable();
                this.Close();
            }
        }
    }
}