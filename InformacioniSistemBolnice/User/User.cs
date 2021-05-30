// File:    User.cs
// Author:  User
// Created: Monday, March 22, 2021 7:10:30 PM
// Purpose: Definition of Class User

using System;

public class User
{
    public String Name { get; set; }
    public String Surname { get; set; }
    public String JMBG { get; set; }
    public Char Gender { get; set; }
    public String PhoneNumber { get; set; }
    public String Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public String Username { get; set; }
    public String Password { get; set; }
    public Boolean IsDeleted { get; set; }



    public ResidentialAddress ResidentialAddress { get; set; }

    public User()
    {
    }
    public User(string name, string surname, string jmbg, char gender, string phoneNumber, string email, DateTime dateOfBirth, string username, string password, ResidentialAddress residentialAddress, Boolean isDeleted = false)
    {
        this.Name = name;
        this.Surname = surname;
        this.JMBG = jmbg;
        this.Gender = gender;
        this.PhoneNumber = phoneNumber;
        this.Email = email;
        this.DateOfBirth = dateOfBirth;
        this.Username = username;
        this.Password = password;
        this.ResidentialAddress = residentialAddress;
        this.IsDeleted = isDeleted;
    }

    public override bool Equals(object obj)
    {
        return obj is User user &&
               Username == user.Username;
    }
}