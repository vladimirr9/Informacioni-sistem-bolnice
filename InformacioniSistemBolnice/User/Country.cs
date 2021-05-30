// File:    Country.cs
// Author:  User
// Created: Saturday, March 27, 2021 2:00:38 PM
// Purpose: Definition of Class Country

using System;

public class Country
{
    public String Name { get; set; }

    public Country(string name)
    {
        this.Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}