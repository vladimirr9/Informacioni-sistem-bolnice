﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.View.ViewModel;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class DoctorPatientsPage : Page
    {
        public DoctorWindow parent;
        private static DoctorPatientsPage instance;

        public DoctorPatientsPage(DoctorWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            this.DataContext = new DoctorPatientsViewModel(parent);
        }

        public static DoctorPatientsPage GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new DoctorPatientsPage(parent);
            return instance;
        }
    }
}
