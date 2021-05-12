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
    /// Interaction logic for PacijentZakazujePoPrioritetuPage.xaml
    /// </summary>
    public partial class PacijentZakazujePoPrioritetuPage : Page
    {
        private PocetnaPacijent parent;
        public PacijentZakazujePoPrioritetuPage(PocetnaPacijent pp)
        {
            parent = pp;
            InitializeComponent();
            parent.UpdateVisibilityOfComponents();
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Visible;
            parent.titlePriorityLabel.Content = "Izaberite prioritet pretrage";
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.startWindow.Content = new PacijentZakazujePage(parent);
        }

        private void doctorRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Main.Content = new PrioritetLjekarPage(parent,this);
        }

        private void timeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Main.Content = new PrioritetVrijemePage(parent,this);
        }
    }
}
