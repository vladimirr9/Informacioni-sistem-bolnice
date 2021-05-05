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
    /// Interaction logic for PacijentZakazujePoPrioritetu.xaml
    /// </summary>
    public partial class PacijentZakazujePoPrioritetu : Window
    {
        private Pacijent pacijent;
        private PacijentWindow parent;
        public PacijentZakazujePoPrioritetu(Pacijent p,PacijentWindow pw)
        {
            this.pacijent = p;
            parent = pw;
            InitializeComponent();
            
        }

        private void doctorRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Main.Content = new PrioritetLjekar(this.pacijent,parent,this);
        }

        private void timeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Main.Content = new PrioritetVrijeme(this.pacijent,parent,this);
        }

       
    }
}
