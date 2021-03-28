// File:    Sekretar.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:32:17 PM
// Purpose: Definition of Class Sekretar

using System;

public class Sekretar : Korisnik
{
    public Sekretar(string ime, string prezime, string jmbg, char pol, string brojTelefona, string email, DateTime datumRodenja, string korisnickoIme, string lozinka, AdresaStanovanja adresaStanovanja, bool isDeleted = false) : base(ime, prezime, jmbg, pol, brojTelefona, email, datumRodenja, korisnickoIme, lozinka, adresaStanovanja, isDeleted)
    {
    }
}

