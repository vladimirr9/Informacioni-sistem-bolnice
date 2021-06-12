using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.View.ViewModel;

namespace InformacioniSistemBolnice.Patient_ns.ViewModelPatient
{
    class StartPatientPageViewModel : BindableBase
    {
        private static StartPatientWindow _parent;
        private PatientController _patientController = new PatientController();
        public MyICommand ShowNotifications { get; set; }
        public MyICommand ShowRatingAppointments { get; set; }
        public MyICommand ShowMedicalRecord { get; set; }
        public MyICommand ShowFeedback { get; set; }
        public MyICommand ShowAppointments { get; set; }

        public StartPatientPageViewModel(StartPatientWindow p)
        {
            _parent = p;
            ShowAppointments = new MyICommand(ShowAppointments_Click);
            ShowFeedback = new MyICommand(ShowFeedback_Click);
            ShowRatingAppointments = new MyICommand(ShowRatingAppointments_Click);
            ShowMedicalRecord = new MyICommand(ShowMedicalRecord_Click);
            ShowNotifications = new MyICommand(ShowNotifications_Click);
        }

        private void ShowAppointments_Click()
        {
            _parent.startWindow.Content = new PatientExaminesAppointmentPage(_parent);
        }

        private void ShowFeedback_Click()
        {
            _parent.startWindow.Content = new PatientFeedbackPage(_parent);
        }

        private void ShowRatingAppointments_Click()
        {
            if (_patientController.CheckStatusOfPatient(_parent.Patient) == true)
            {
                MessageBox.Show("Ova funkcionalnost Vam je trenutno onemogućena,obratite se sekretaru!", "Greška");
            }
            else
            {
                _parent.startWindow.Content = new RatingPage(_parent);
            }
        }

        private void ShowMedicalRecord_Click()
        {
            _parent.startWindow.Content = new PatientMedicalRecordPage(_parent);
        }

        private void ShowNotifications_Click()
        {
            _parent.startWindow.Content = new NotificationPatientPage(_parent);
        }
    }
}
