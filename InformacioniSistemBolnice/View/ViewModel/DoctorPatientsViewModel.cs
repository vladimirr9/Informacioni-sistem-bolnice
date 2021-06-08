using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Doctor_ns;

namespace InformacioniSistemBolnice.View.ViewModel
{
    class DoctorPatientsViewModel : BindableBase
    {
        public DoctorWindow parent;
        private ObservableCollection<Patient> patients;
        private Patient selectedPatient;
        public MyICommand MedicalRecordCommand { get; set; }
        private PatientController _patientController = new PatientController();

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
            }
        }

        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            set
            {
                patients = value;
                OnPropertyChanged("Patients");
            }
        }

        public DoctorPatientsViewModel(DoctorWindow parent)
        {
            this.parent = parent;
            MedicalRecordCommand = new MyICommand(Medical_Record_Click);
            UpdateTable();
        }

        private void Medical_Record_Click()
        {
            if (selectedPatient != null)
            {
                MedicalRecordWindow recordWindow = new MedicalRecordWindow(selectedPatient, parent);
                Application.Current.MainWindow = recordWindow;
                recordWindow.Show();
            }
        }

        public void UpdateTable()
        {
            List<Patient> patients = new List<Patient>();
            foreach (Patient patient in _patientController.GetAll())
            {
                patients.Add(patient);
            }

            Patients = new ObservableCollection<Patient>(patients);
            OnPropertyChanged("Patients");
        }
    }
}
