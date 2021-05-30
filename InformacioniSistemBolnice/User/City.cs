// File:    City.cs
// Author:  User
// Created: Saturday, March 27, 2021 2:00:38 PM
// Purpose: Definition of Class City

using System;

public class City
{
    public String Name { get; set; }
    public String PostalCode { get; set; }

    public Country Country;

    public City(string name, string postalCode, Country country)
    {
        this.Name = name;
        this.PostalCode = postalCode;
        this.Country = country;
    }

    public override string ToString()
    {
        return Name + ", " + Country.ToString();
    }
}