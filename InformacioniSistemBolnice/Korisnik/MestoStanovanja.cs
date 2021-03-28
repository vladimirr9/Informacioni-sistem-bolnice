// File:    MestoStanovanja.cs
// Author:  Korisnik
// Created: Saturday, March 27, 2021 2:00:38 PM
// Purpose: Definition of Class MestoStanovanja

using System;

public class MestoStanovanja
{
    public String naziv { get; set; }
    public String postanskiBroj { get; set; }

    public DrzavaStanovanja drzavaStanovanja;

    public MestoStanovanja(string naziv, string postanskiBroj, DrzavaStanovanja drzavaStanovanja)
    {
        this.naziv = naziv;
        this.postanskiBroj = postanskiBroj;
        this.drzavaStanovanja = drzavaStanovanja;
    }

    public override string ToString()
    {
        return naziv + ", " + drzavaStanovanja.ToString();
    }
}