// File:    DrzavaStanovanja.cs
// Author:  Korisnik
// Created: Saturday, March 27, 2021 2:00:38 PM
// Purpose: Definition of Class DrzavaStanovanja

using System;

public class DrzavaStanovanja
{
    public String naziv { get; set; }

    public DrzavaStanovanja(string naziv)
    {
        this.naziv = naziv;
    }

    public override string ToString()
    {
        return naziv;
    }
}