using System;

namespace InformacioniSistemBolnice.Secretary_ns
{
    public class Secretary : global::User
    {
        public Secretary(string name, string surname, string jmbg, char gender, string phoneNumber, string email, DateTime dateOfBirth, string username, string password, ResidentialAddress residentialAddress, bool isDeleted = false) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, username, password, residentialAddress, isDeleted)
        {
        }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}