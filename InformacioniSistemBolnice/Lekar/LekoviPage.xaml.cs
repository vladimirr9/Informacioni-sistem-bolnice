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

namespace InformacioniSistemBolnice.Lekar
{
    /// <summary>
    /// Interaction logic for LekoviPage.xaml
    /// </summary>
    public partial class LekoviPage : Page
    {
        public LekarWindow parent;
        private static LekoviPage instance;

        public LekoviPage(LekarWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            UpdateList();
            SastavList.Items.Clear();
        }

        public static LekoviPage GetPage(LekarWindow parent)
        {
            if (instance == null)
                instance = new LekoviPage(parent);
            return instance;
        }

        private void LekoviZaPotvrduClick(object sender, RoutedEventArgs e)
        {
            LekoviZaPotvrduWindow lekoviWindow = new LekoviZaPotvrduWindow();
            Application.Current.MainWindow = lekoviWindow;
            lekoviWindow.Show();
        }

        private void DodajSastojakClick(object sender, RoutedEventArgs e)
        {
            if (Sastojci.SelectedIndex != -1)
            {
                List<Medicine> lekovi = MedicineFileRepository.GetAll();
                foreach (Medicine l in lekovi)
                {
                    if (l.Name == LekoviList.SelectedItem.ToString())
                    {
                        l.IngredientsList.Add((Ingredient)Sastojci.SelectedItem);
                        //dodati u storage
                        IspisiSastojke(l);
                    }
                }
            }
        }

        private void UpdateList()
        {
            LekoviList.Items.Clear();
            List<Medicine> lekovi = MedicineFileRepository.GetAll();
            foreach (Medicine l in lekovi)
            {
                if (!l.IsDeleted && l.MedicineStatus.Equals(MedicineStatus.validated))
                    LekoviList.Items.Add(l.Name);
            }
        }

        public void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            SastavList.Items.Clear();
            foreach (Medicine lek in MedicineFileRepository.GetAll())
            {
                if (lek.Name == LekoviList.SelectedItem.ToString())
                {
                    IspisiSastojke(lek);
                }
            }
        }

        private void IspisiSastojke(Medicine lek)
        {
            SastavList.Items.Clear();
            List<Ingredient> sastojci = new List<Ingredient>();
            foreach (Ingredient sastojak in IngredientFileStorage.GetAll())
            {
                if (lek.IngredientsList.Contains(sastojak))
                {
                    SastavList.Items.Add(sastojak.Name);
                }
                else
                {
                    sastojci.Add(sastojak);
                }

            }
            Sastojci.ItemsSource = sastojci;
        }
    }
}
