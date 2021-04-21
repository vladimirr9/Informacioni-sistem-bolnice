using InformacioniSistemBolnice.Korisnik;
using InformacioniSistemBolnice.Lekar;
using InformacioniSistemBolnice.Sekretar_ns;
using InformacioniSistemBolnice.Upravnik;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //LekarFileStorage.AddLekar(new global::Lekar("Petar", "Petrovic", "1507970600034", 'm', "+381661238407", "ppetrovic@gmail.com", new DateTime(1970,4,12), "PPetrovic", "petrovic123", null, TipLekara.specijalista, false));
            //SekretarFileStorage.AddSekretar(new Sekretar("Vladimir", "Rokvic", "0405993705030", 'M', "+32432343232", "neko@nesto.com", new DateTime(), "vladimir", "rokvic", new AdresaStanovanja("Kralja Petra 1", new MestoStanovanja("Novi Sad", "23223", new DrzavaStanovanja("Republika Srbija"))), false));
            //PacijentFileStorage.AddPacijent(new Pacijent("Pera","Peric", "09320434533",'M',"+3245344323","Pera@peric.com",new DateTime(),"pera","peric",new AdresaStanovanja("Kralja Petra 12", new MestoStanovanja("Novi Sad", "23232", new DrzavaStanovanja("Republika Srbija"))),false,"2323224343",new List<Termin>(),new ZdravstveniKarton("232", null),false));
            InitializeComponent();

            global::Lekar l = LekarFileStorage.GetOne("PPetrovic");
            Console.WriteLine(l.IsAvailable(DateTime.Parse("15-Apr-21"), DateTime.Parse("16-Apr-21" + " " + "10:05")));
            Console.WriteLine(DateTime.Parse("16-Apr-21"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Pacijent> pacijenti = PacijentFileStorage.GetAll();
            List<Termin> termini = TerminFileStorage.GetAll();
            foreach (var pacijent in pacijenti)
            {
                if (ime.Text.Equals(pacijent.korisnickoIme)  && lozinka.Password.Equals(pacijent.lozinka))
                {

                    String ime = pacijent.korisnickoIme;
                            
                            PocetnaPacijent pp = new PocetnaPacijent(pacijent);
                            Application.Current.MainWindow = pp;
                            pp.Show();
                            this.Close();
                            return;
                        
                    
                }
            }
            List<Sekretar> sekretari = SekretarFileStorage.GetAll();
            foreach (Sekretar s in sekretari)
            {
                if (ime.Text.Equals(s.korisnickoIme) && lozinka.Password.Equals(s.lozinka))
                {
                    SekretarMain sw = new SekretarMain(s);
                    Application.Current.MainWindow = sw;
                    sw.Main.Content = PocetnaPage.GetPage(s);
                    sw.Show();
                    this.Close();
                    return;
                }
            }
            List<global::Lekar> lekari = LekarFileStorage.GetAll();
            foreach (global::Lekar l in lekari)
            {
                if (ime.Text.Equals(l.korisnickoIme) && lozinka.Password.Equals(l.lozinka))
                {
                    LekarWindow lw = new LekarWindow(l);
                    Application.Current.MainWindow = lw;
                    lw.Show();
                    this.Close();
                    return;
                }
            }

            List<global::Upravnik> upravnikLista = UpravnikFileStorage.GetAll();
            foreach (global::Upravnik u in upravnikLista)
            {
                if (ime.Text.Equals(u.korisnickoIme) && lozinka.Password.Equals(u.lozinka))
                {
                    UpravnikWindow uw = new UpravnikWindow();
                    Application.Current.MainWindow = uw;
                    uw.Show();
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("Neuspjesno logovanje!");
        }
    }
}
