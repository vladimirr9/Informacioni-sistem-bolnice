using System.Collections.Generic;
using System.Windows;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.Secretary_ns.ViewModel;

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class DetailedWindow : Window
    {

        public DetailedWindow(PatientsPage parent, string username)
        {
            InitializeComponent();
            DataContext = new DetailedViewModel(this, username);
        }

    }
}