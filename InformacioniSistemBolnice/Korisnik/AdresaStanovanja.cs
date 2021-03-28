// File:    AdresaStanovanja.cs
// Author:  Korisnik
// Created: Saturday, March 27, 2021 2:00:37 PM
// Purpose: Definition of Class AdresaStanovanja

using System;

public class AdresaStanovanja
{
    public String ulicaIBroj { get; set; }

    public MestoStanovanja mestoStanovanja { get; set; }

    public AdresaStanovanja(string ulicaIBroj, MestoStanovanja mestoStanovanja)
    {
        this.ulicaIBroj = ulicaIBroj;
        this.mestoStanovanja = mestoStanovanja;
    }

    public override string ToString()
    {
        return ulicaIBroj + ", " + mestoStanovanja.ToString();
    }
}