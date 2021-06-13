using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.Patient_ns;

namespace InformacioniSistemBolnice.Factory
{
    class PatientLoginer : ILoginer
    {
        private Patient _patient;
        public PatientLoginer(Patient patient)
        {
            this._patient = patient;
        }

        public void Login()
        {
            StartPatientWindow patientWindow = new StartPatientWindow(_patient);
            Application.Current.MainWindow = patientWindow;
            patientWindow.startWindow.Content = new StartPatientPage(patientWindow);
            patientWindow.Show();
        }
    }
}
