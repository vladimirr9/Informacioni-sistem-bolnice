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

        private AppointmentsPage()
        {
            InitializeComponent();
            this.DataContext = new AppointmentsPageViewModel();

        }
        public static AppointmentsPage GetPage(SecretaryMain parent)
        {
            parent.Title.Content = "Termini";
            return new AppointmentsPage();
        }
    }
}
