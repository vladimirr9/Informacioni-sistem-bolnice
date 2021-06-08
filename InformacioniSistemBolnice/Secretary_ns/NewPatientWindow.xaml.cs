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
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Secretary_ns.ViewModel;

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class NewPatientWindow : Window
    {

        public NewPatientWindow(PatientsPage parent)
        {
            InitializeComponent();
            this.DataContext = new NewPatientViewModel(this);
        }

    }
}
