// File:    ResidentialAddress.cs
// Author:  User
// Created: Saturday, March 27, 2021 2:00:37 PM
// Purpose: Definition of Class ResidentialAddress

using System;

public class ResidentialAddress
{
    public String StreetAndNumber { get; set; }

    public City City { get; set; }

    public ResidentialAddress(string streetAndNumber, City city)
    {
        this.StreetAndNumber = streetAndNumber;
        this.City = city;
    }

    public override string ToString()
    {
        return StreetAndNumber + ", " + City.ToString();
    }
}