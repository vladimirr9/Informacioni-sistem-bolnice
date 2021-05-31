using InformacioniSistemBolnice.Controller;
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

namespace InformacioniSistemBolnice.Upravnik
{
    /// <summary>
    /// Interaction logic for LekoviWindow.xaml
    /// </summary>
    public partial class LekoviWindow : Window
    {
        private UpravnikWindow _parent;
        private MedicineController _medicineController = new MedicineController();
        public LekoviWindow(UpravnikWindow parent)
        {
            InitializeComponent();
            this._parent = parent;
            UpdateTable();

            this.DataContext = this;
            SastojciLeka.Items.Clear();
        }

        private void ObrisiLek(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedItem != null)
            {
                MessageBoxResult answer = MessageBox.Show("Da li želite da obrišete selektovani lek?", "Potvrda brisanja leka", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    Medicine selectedMedicine = (Medicine)dataGridLekovi.SelectedItem;
                    _medicineController.SendMedicineForRemovingValidation(selectedMedicine);
                    //selectedMedicine.MedicineStatus = MedicineStatus.waitingForValidation;
                    //LekFileStorage.RemoveLek(selektovan.Naziv);
                    //dataGridLekovi.Items.Remove(dataGridLekovi.SelectedItem);
                    MessageBox.Show("Lek poslat lekaru na validaciju brisanja!", "Čekanje na validaciju", MessageBoxButton.OK);
                    this.Close();
                }
            }
            UpdateTable();
        }

        private void IzmeniLek(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedItem != null)
            {
                Medicine selectedMedicine = (Medicine)dataGridLekovi.SelectedItem;
                if (selectedMedicine.MedicineStatus == MedicineStatus.validated || selectedMedicine.MedicineStatus == MedicineStatus.rejected)
                {
                    IzmenaLekaWindow updateMedicineWindow = new IzmenaLekaWindow(selectedMedicine, this);
                    updateMedicineWindow.Show();
                }
                else
                {
                    MessageBox.Show("Ne možete menjati lek koji je na čekanju za validaciju.", "Upozorenje!", MessageBoxButton.OK);
                }

            }
        }

        private void DodajNoviLek(object sender, RoutedEventArgs e)
        {
            DodavanjeLekaWindow addMedicineWindow = new DodavanjeLekaWindow(this);
            addMedicineWindow.Show();
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void UpdateTable()
        {
            dataGridLekovi.Items.Clear();
            foreach (Medicine med in _medicineController.GetAllMedicines())
            {
                if (!med.IsDeleted)
                    dataGridLekovi.Items.Add(med);
            }
        }

        private void dataGridLekovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SastojciLeka.Items.Clear();
            Medicine selectedMedicine = (Medicine)dataGridLekovi.SelectedItem;
            foreach(Ingredient ingredient in selectedMedicine.IngredientsList)
            {
                SastojciLeka.Items.Add(ingredient);
            }
            /*List<Ingredient> ingredients = _medicineController.GetMedicineIngredients(selectedMedicine);
            SastojciLeka.ItemsSource = ingredients;*/
        }
    }
}
