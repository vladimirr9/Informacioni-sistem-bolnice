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

    public Boolean Banovan { get; set; } = false;

    public DateTime TrenutakBanovanja { get; set; }

    public ZdravstveniKarton zdravstveniKarton { get; set; }


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


    public bool IsAvailable(DateTime pocetak, DateTime kraj) // proverava da li je PatientComboBox slobodan izmedju neka dva trenutka u vremenu
    {
        if (pocetak.Equals(kraj))
            return true;
        bool retVal = true;
        List<Termin> termini = TerminFileStorage.GetAll();
        foreach (Termin termin in termini)
        {
            if (termin.Pacijent.Equals(this) && termin.status == StatusTermina.zakazan)
            {
                if (pocetak >= termin.datumZakazivanja && pocetak <= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (kraj >= termin.datumZakazivanja && kraj <= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (pocetak <= termin.datumZakazivanja && kraj >= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
            }
        }
        return retVal;
    }

    public override bool Equals(object obj)
    {
        return obj is Pacijent pacijent &&
               base.Equals(obj);
    }
}