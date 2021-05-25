using System;

namespace InformacioniSistemBolnice.Secretary_ns
{
    public class Secretary : global::Korisnik
    {
        public Secretary(string ime, string prezime, string jmbg, char pol, string brojTelefona, string email, DateTime datumRodenja, string korisnickoIme, string lozinka, AdresaStanovanja adresaStanovanja, bool isDeleted = false) : base(ime, prezime, jmbg, pol, brojTelefona, email, datumRodenja, korisnickoIme, lozinka, adresaStanovanja, isDeleted)
        {
        }
    }
}