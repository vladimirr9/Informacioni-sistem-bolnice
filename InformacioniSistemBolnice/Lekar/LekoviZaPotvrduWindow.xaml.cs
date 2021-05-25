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
    /// Interaction logic for LekoviZaPotvrduWindow.xaml
    /// </summary>
    public partial class LekoviZaPotvrduWindow : Window
    {
        public LekoviZaPotvrduWindow()
        {
            InitializeComponent();
            UpdateList();
            SastavList.Items.Clear();
        }

        private void ValidirajClick(object sender, RoutedEventArgs e)
        {
            if (LekoviList.SelectedItem != null)
            {
                String naziv = LekoviList.SelectedItem.ToString();
                List<Lek> lekovi = LekFileStorage.GetAll();
                foreach (Lek l in lekovi)
                {
                    if (l.Naziv == naziv)
                    {
                        l.StatusLeka = StatusLeka.validiran;
                    }
                }
                UpdateList();
            }
        }

        private void VratiClick(object sender, RoutedEventArgs e)
        {
            Izmena.Clear();
            UpdateList();
        }

        private void UpdateList()
        {
            LekoviList.Items.Clear();
            List<Lek> lekovi = LekFileStorage.GetAll();
            foreach (Lek l in lekovi)
            {
                if (!l.IsDeleted && l.StatusLeka.Equals(StatusLeka.cekaNaValidaciju))
                    LekoviList.Items.Add(l.Naziv);
            }
        }

        public void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            SastavList.Items.Clear();
            String naziv = LekoviList.SelectedItem.ToString();
            List<Lek> lekovi = LekFileStorage.GetAll();
            foreach (Lek l in lekovi)
            {
                if (l.Naziv == naziv)
                {
                    Console.WriteLine(naziv);
                    foreach (Ingredient s in l.ListaSastojaka)
                    {
                        SastavList.Items.Add(s.Name);
                    }
                }
            }
        }
    }
}
