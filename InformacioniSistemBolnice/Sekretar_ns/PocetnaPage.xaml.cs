using InformacioniSistemBolnice.Korisnik;
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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for PocetnaPage.xaml
    /// </summary>
    public partial class PocetnaPage : Page
    {
        public List<Obavestenje> obavestenja { get; set; }
        private Sekretar tekSekretar;
        private static PocetnaPage instance;
        private PocetnaPage(Sekretar tekSekretar)
        {
            this.tekSekretar = tekSekretar;
            
            InitializeComponent();
            updateTable();
        }

        public static PocetnaPage GetPage(Sekretar tekSekretar)
        {
            if (instance == null)
                instance = new PocetnaPage(tekSekretar);
            else
                instance.updateTable();
            return instance;
        }

        

        private void Novo_Click(object sender, RoutedEventArgs e)
        {
            NovoObavestenjeWindow window = new NovoObavestenjeWindow(this);
            window.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (PrikazObavestenja.SelectedItem != null && ((Obavestenje)PrikazObavestenja.SelectedItem).Recipients.Contains("ALL_USERS"))
            {
                Obavestenje inicijalnoObavestenje = ObavestenjeFileStorage.GetOne(((Obavestenje)(PrikazObavestenja.SelectedItem)).idObavestenja);
                IzmeniObavestenjeWindow window = new IzmeniObavestenjeWindow(this, inicijalnoObavestenje);
                window.Show();
            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (PrikazObavestenja.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da obrišete ovo obavestenje?", "Potvrda brisanja", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    int id = ((Obavestenje)PrikazObavestenja.SelectedItem).idObavestenja;
                    ObavestenjeFileStorage.RemoveObavestenje(id);
                    updateTable();
                }
            }
        }

        public void updateTable()
        {
            obavestenja = new List<Obavestenje>();
            foreach (Obavestenje notification in ObavestenjeFileStorage.GetAll())
            {
                if (notification.IsDirectedTo(tekSekretar.korisnickoIme))
                {
                    if (!notification.isDeleted)
                        obavestenja.Add(notification);
                }
            }
            PrikazObavestenja.ItemsSource = obavestenja;
        }


        public class CustomMultiValueConvertor : IMultiValueConverter

        {
            public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return String.Concat(values[0], " ", values[1]);
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
            {
                return (value as string).Split(' ');
            }
        }
        
    }
}
