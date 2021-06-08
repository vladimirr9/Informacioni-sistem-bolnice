using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Reports;
using InformacioniSistemBolnice.Upravnik;
using InformacioniSistemBolnice.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Manager_ns.ViewModel
{
    class MedicinesViewModel : BindableBase
    {
        private Medicine selectedMedicine;
        private ObservableCollection<Medicine> medicines;
        private ObservableCollection<Ingredient> ingredients;
        private UpravnikWindow _parent;
        private LekoviWindow _thisWindow;
        private MedicineController _medicineController = new MedicineController();
        private PrintDialog _printDialog = new PrintDialog();
        private MyICommand DeleteMedicineCommand { get; set; }
        private MyICommand AddMedicineCommand { get; set; }
        private MyICommand UpdateMedicineCommand { get; set; }
        private MyICommand GenerateReportCommand { get; set; }
        private MyICommand CancelCommand { get; set; }
        public MedicinesViewModel(UpravnikWindow parent, LekoviWindow thisWindow)
        {
            _parent = parent;
            _thisWindow = thisWindow;
            DeleteMedicineCommand = new MyICommand(ObrisiLek);
            AddMedicineCommand = new MyICommand(DodajNoviLek);
            UpdateMedicineCommand = new MyICommand(IzmeniLek);
            GenerateReportCommand = new MyICommand(GenerateReport);
            CancelCommand = new MyICommand(Zatvori);
            UpdateTable();
        }

        public Medicine SelectedMedicine
        {
            get { return selectedMedicine; }
            set
            {
                this.selectedMedicine = value;
                dataGridLekovi_SelectionChanged();
                OnPropertyChanged("SelectedMedicine");
            }
        }
        public ObservableCollection<Medicine> Medicines
        {
            get { return medicines; }
            set
            {
                this.medicines = value;
                OnPropertyChanged("Medicines");
            }
        }

        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set
            {
                this.ingredients = value;
                OnPropertyChanged("Ingredients");
            }
        }

        private void dataGridLekovi_SelectionChanged()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            if (selectedMedicine != null)
            {
                foreach (Ingredient ingredient in _medicineController.GetMedicineIngredients(selectedMedicine))
                {
                    ingredients.Add(ingredient);
                }

                Ingredients = new ObservableCollection<Ingredient>(ingredients);
                OnPropertyChanged("Ingredients");
            }
        }

        private void ObrisiLek()
        {
            if (selectedMedicine != null)
            {
                MessageBoxResult answer = MessageBox.Show("Da li želite da obrišete selektovani lek?", "Potvrda brisanja leka", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    //Medicine selectedMedicine = selectedMedicine;
                    _medicineController.SendMedicineForRemovingValidation(selectedMedicine);
                    //selectedMedicine.MedicineStatus = MedicineStatus.waitingForValidation;
                    //LekFileStorage.RemoveLek(selektovan.Naziv);
                    //dataGridLekovi.Items.Remove(dataGridLekovi.SelectedItem);
                    MessageBox.Show("Lek poslat lekaru na validaciju brisanja!", "Čekanje na validaciju", MessageBoxButton.OK);
                    _thisWindow.Close();
                }
            }
            UpdateTable();
        }

        private void IzmeniLek()
        {
            if (selectedMedicine != null)
            {
                //Medicine selectedMedicine = selectedMedicine;
                if (selectedMedicine.MedicineStatus == MedicineStatus.validated || selectedMedicine.MedicineStatus == MedicineStatus.rejected)
                {
                    IzmenaLekaWindow updateMedicineWindow = new IzmenaLekaWindow(selectedMedicine, _thisWindow);
                    updateMedicineWindow.Show();
                }
                else
                {
                    MessageBox.Show("Ne možete menjati lek koji je na čekanju za validaciju.", "Upozorenje!", MessageBoxButton.OK);
                }

            }
        }

        private void DodajNoviLek()
        {
            DodavanjeLekaWindow addMedicineWindow = new DodavanjeLekaWindow(_thisWindow);
            addMedicineWindow.Show();
        }

        private void Zatvori()
        {
            _thisWindow.Close();
        }
        private void GenerateReport()
        {
            _printDialog.PrintVisual(new MedicinesReport(), "Izveštaj 1");
        }

        public void UpdateTable()
        {
            /*Medicines.Clear();
            foreach (Medicine med in _medicineController.GetAllMedicines())
            {
                if (!med.IsDeleted)
                    Medicines.Add(med);
            }*/
            List<Medicine> medicines = new List<Medicine>();
            foreach (Medicine medicine in _medicineController.GetAllMedicines())
            {
                if (!medicine.IsDeleted && medicine.MedicineStatus.Equals(MedicineStatus.waitingForValidation))
                    medicines.Add(medicine);
            }

            Medicines = new ObservableCollection<Medicine>(medicines);
            OnPropertyChanged("Medicines");
        }
    }
}

/*SastojciLeka.Items.Clear();
Medicine selectedMedicine = (Medicine)dataGridLekovi.SelectedItem;
if (selectedMedicine != null)
{
    foreach (Ingredient ingredient in selectedMedicine.IngredientsList)
    {
        SastojciLeka.Items.Add(ingredient);
    }
}*/
