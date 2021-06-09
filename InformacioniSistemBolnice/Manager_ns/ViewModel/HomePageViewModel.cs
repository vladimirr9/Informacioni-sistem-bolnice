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
        public MyICommand EmployeesCommand { get; set; }
        public MyICommand DemoCommand { get; set; }
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
            EmployeesCommand = new MyICommand(Employees);
            DemoCommand = new MyICommand(Demo);
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
        private void Employees()
        {
            EmployeesWindow ew = new EmployeesWindow(parent);
            ew.Show();
        }

        private void Demo()
        {
            var t1 = Task.Delay(3000);
            t1.Wait();
            WindowProstorije wp = new WindowProstorije(parent);
            wp.Show();
            //parent.button.Command.CanExecute(RoomCommand);
            var t2 = Task.Delay(3000);
            t2.Wait();
            Room room = (Room)wp.datagridProstorije.SelectedItem;
            OpremaWindow ow = new OpremaWindow(room, wp);
            ow.Show();
            var t3 = Task.Delay(3000);
            t3.Wait();
            ow.Close();
            var t5 = Task.Delay(3000);
            t5.Wait();
            wp.Close();
        }
    }
}
