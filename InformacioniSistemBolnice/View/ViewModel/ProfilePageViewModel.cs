using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Doctor_ns;

namespace InformacioniSistemBolnice.View.ViewModel
{
    class ProfilePageViewModel : BindableBase
    {
        public DoctorWindow parent;
        public String Name { get; set; }
        public String Title { get; set; }
        public String Date { get; set; }
        public String JMBG { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        private DoctorControler _doctorController = new DoctorControler();

        public ProfilePageViewModel(DoctorWindow parent)
        {
            this.parent = parent;
            Name= parent.Doctor.Name + " " + parent.Doctor.Surname;
            Title = _doctorController.GetType(parent.Doctor);
            Date = parent.Doctor.DateOfBirth.Date.ToString();
            JMBG = parent.Doctor.JMBG;
            Address = parent.Doctor.ResidentialAddress.ToString();
            Email = parent.Doctor.Email;
        }
    }
}
