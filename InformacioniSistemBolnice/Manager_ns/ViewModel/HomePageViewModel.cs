using InformacioniSistemBolnice.Upravnik;
using InformacioniSistemBolnice.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Manager_ns.ViewModel
{
    class HomePageViewModel : BindableBase
    {
        public String Name { get; set; }
        public String Date { get; set; }
        public String Address { get; set; }
        public String JMBG { get; set; }
        public String Email { get; set; }
        public String Number { get; set; }
        private Manager _loggedManager;
        private UpravnikWindow parent;

        public MyICommand RoomCommand { get; set; }
        public MyICommand MedicinesCommand { get; set; }
        public MyICommand LogOutCommand { get; set; }
        public MyICommand EditProfileCommand { get; set; }

        public HomePageViewModel(Manager manager, UpravnikWindow parent)
        {
            _loggedManager = manager;
            this.parent = parent;
            Name = _loggedManager.Name + " " + _loggedManager.Surname;
            Date = _loggedManager.DateOfBirth.Date.ToString();
            Address = _loggedManager.ResidentialAddress.StreetAndNumber;
            JMBG = _loggedManager.JMBG;
            Email = _loggedManager.Email;
            Number = _loggedManager.PhoneNumber;

            RoomCommand = new MyICommand(WindowProstorije);
            MedicinesCommand = new MyICommand(LekoviWindow);
            LogOutCommand = new MyICommand(Pocetna);
            EditProfileCommand = new MyICommand(EditProfile);
        }
        private void WindowProstorije()
        {
            WindowProstorije wp = new WindowProstorije(parent);
            wp.Show();
        }

        private void LekoviWindow()
        {
            LekoviWindow lw = new LekoviWindow(parent);
            lw.Show();
        }

        private void Pocetna()
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            parent.Close();
        }
        private void EditProfile()
        {
            EditProfileWindow window = new EditProfileWindow(parent);
            window.Show();
        }
    }
}
