using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Factory
{
    class DoctorLoginer : ILoginer
    {
        private Doctor _doctor;
        public DoctorLoginer(Doctor doctor)
        {
            this._doctor = doctor;
        }

        public void Login()
        {
            DoctorWindow doctorWindow = new DoctorWindow(_doctor);
            Application.Current.MainWindow = doctorWindow;
            doctorWindow.Show();
        }
    }
}
