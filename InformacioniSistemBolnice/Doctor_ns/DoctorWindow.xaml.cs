﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
using InformacioniSistemBolnice.Doctor_ns;
using InformacioniSistemBolnice.View.ViewModel;

namespace InformacioniSistemBolnice
{
    public partial class DoctorWindow : Window
    {
        public Doctor Doctor;
        public DoctorWindow(Doctor doctor)
        {
            this.Doctor = doctor;
            InitializeComponent();
            this.DataContext = new DoctorWindowViewModel(doctor, this);
        }
    }
}
