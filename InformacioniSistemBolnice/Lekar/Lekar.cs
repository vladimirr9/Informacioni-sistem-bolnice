// File:    Lekar.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:32:18 PM
// Purpose: Definition of Class Lekar

using System;

public class Lekar : Korisnik
{
    public TipLekara tipLekara { get; set; }
    //private int iDLekara;

    public Lekar(string ime, string prezime, string jmbg, char pol, string brojTelefona, string email, DateTime datumRodenja, string korisnickoIme, string lozinka, AdresaStanovanja adresaStanovanja, TipLekara tipLekara, bool isDeleted = false) : base(ime, prezime, jmbg, pol, brojTelefona, email, datumRodenja, korisnickoIme, lozinka, adresaStanovanja, isDeleted)
    {
        this.tipLekara = tipLekara;
    }
}