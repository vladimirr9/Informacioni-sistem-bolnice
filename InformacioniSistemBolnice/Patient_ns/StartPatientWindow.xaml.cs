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
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for StartPatientWindow.xaml
    /// </summary>
    public partial class StartPatientWindow : Window
    {
        public Patient Patient { get; set; }

        public StartPatientWindow(Patient patient)
        {
            this.Patient = patient;
            InitializeComponent();
            imePacijenta.Text = patient.Name + " " + patient.Surname;



        }

        private void odjava_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            Application.Current.MainWindow = m;
            m.Show();
            this.Close();
            return;
        }

        public void UpdateVisibilityOfComponents()
        {
            iconAndName.Visibility = Visibility.Hidden;
            odjava.Visibility = Visibility.Hidden;
        }
    }
}
