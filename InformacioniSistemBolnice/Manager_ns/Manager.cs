// File:    Upravnik.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:32:16 PM
// Purpose: Definition of Class Upravnik

using System;

public class Manager : Korisnik
{
   public Manager(string ime, string prezime, string jmbg, char pol, string brojTelefona, string email, DateTime datumRodenja, string korisnickoIme, string lozinka, AdresaStanovanja adresaStanovanja, bool isDeleted = false) : base(ime, prezime, jmbg, pol, brojTelefona, email, datumRodenja, korisnickoIme, lozinka, adresaStanovanja, isDeleted)
    {

    }

}