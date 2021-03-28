// File:    Korisnik.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 7:10:30 PM
// Purpose: Definition of Class Korisnik

using System;

public class Korisnik
{
    public String ime { get; set; }
    public String prezime { get; set; }
    public String jmbg { get; set; }
    public Char pol { get; set; }
    public String brojTelefona { get; set; }
    public String email { get; set; }
    public DateTime datumRodenja { get; set; }
    public String korisnickoIme { get; set; }
    public String lozinka { get; set; }
    public Boolean isDeleted { get; set; }

    public AdresaStanovanja adresaStanovanja { get; set; }

    public Korisnik()
    {
    }
    public Korisnik(string ime, string prezime, string jmbg, char pol, string brojTelefona, string email, DateTime datumRodenja, string korisnickoIme, string lozinka, AdresaStanovanja adresaStanovanja, Boolean isDeleted = false)
    {
        this.ime = ime;
        this.prezime = prezime;
        this.jmbg = jmbg;
        this.pol = pol;
        this.brojTelefona = brojTelefona;
        this.email = email;
        this.datumRodenja = datumRodenja;
        this.korisnickoIme = korisnickoIme;
        this.lozinka = lozinka;
        this.adresaStanovanja = adresaStanovanja;
        this.isDeleted = isDeleted;
    }
}