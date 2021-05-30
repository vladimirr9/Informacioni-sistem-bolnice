// File:    Upravnik.cs
// Author:  User
// Created: Monday, March 22, 2021 6:32:16 PM
// Purpose: Definition of Class Upravnik

using System;

public class Manager : User
{
   public Manager(string name, string surname, string jmbg, char gender, string phoneNumber, string email, DateTime dateOfBirth, string username, string password, ResidentialAddress residentialAddress, bool isDeleted = false) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, username, password, residentialAddress, isDeleted)
    {

    }

}