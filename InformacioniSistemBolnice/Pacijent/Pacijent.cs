// File:    Pacijent.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:32:17 PM
// Purpose: Definition of Class Pacijent

using System;
using System.Collections.Generic;

public class Pacijent : Korisnik
{
    public Boolean isGuest { get; set; }
    public String brojZdravstveneKartice { get; set; }

    

    public ZdravstveniKarton zdravstveniKarton;


    public Pacijent(
      string ime,
      string prezime,
      string jmbg,
      char pol,
      string brojTelefona,
      string email,
      DateTime datumRodenja,
      string korisnickoIme,
      string lozinka,
      AdresaStanovanja adresaStanovanja, bool isGuest, string brojZdravstveneKartice, ZdravstveniKarton zdravstveniKarton,
      bool isDeleted = false) : base(ime, prezime, jmbg, pol, brojTelefona, email, datumRodenja, korisnickoIme, lozinka, adresaStanovanja, isDeleted)
    {
        this.isGuest = isGuest;
        this.brojZdravstveneKartice = brojZdravstveneKartice;
        this.zdravstveniKarton = zdravstveniKarton;
    }

}