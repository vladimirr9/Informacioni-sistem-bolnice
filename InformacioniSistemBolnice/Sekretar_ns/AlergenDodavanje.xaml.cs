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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for AlergenDodavanje.xaml
    /// </summary>
    public partial class AlergenDodavanje : Window
    {
        private DetaljnijeWindow parent;
        private Pacijent pacijent;
        private List<Sastojak> alergeni;
        public AlergenDodavanje(Pacijent pacijent, DetaljnijeWindow parent)
        {
            this.parent = parent;
            this.pacijent = pacijent;
            alergeni = new List<Sastojak>();
            foreach (Sastojak a in SastojakFileStorage.GetAll())
            {
                if (!pacijent.zdravstveniKarton.Alergen.Contains(a))
                    alergeni.Add(a);
            }
            InitializeComponent();
            AlergeniList.ItemsSource = alergeni;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            pacijent.zdravstveniKarton.AddSastojak((Sastojak)AlergeniList.SelectedItem);
            PacijentFileStorage.UpdatePacijent(pacijent.korisnickoIme, pacijent);
            parent.updateTable();
            Close();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
