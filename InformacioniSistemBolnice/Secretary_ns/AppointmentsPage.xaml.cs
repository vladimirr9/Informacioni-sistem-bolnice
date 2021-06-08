using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Secretary_ns.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Secretary_ns
{

    public partial class AppointmentsPage : Page
    {

        private static AppointmentsPage _instance;
        private AppointmentsPage()
        {
            InitializeComponent();
            this.DataContext = new AppointmentsPageViewModel();

        }
        public static AppointmentsPage GetPage(SecretaryMain parent)
        {
            if (_instance == null)
                _instance = new AppointmentsPage();
            parent.Title.Content = "Termini";
            return _instance;
        }
    }
}
