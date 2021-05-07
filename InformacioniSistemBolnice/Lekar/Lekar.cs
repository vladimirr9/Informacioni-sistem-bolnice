// File:    Lekar.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:32:18 PM
// Purpose: Definition of Class Lekar

using System;
using System.Collections.Generic;
using System.Windows.Documents;

public class Lekar : Korisnik
{
    public TipLekara tipLekara { get; set; }
    //private int iDLekara;

    public Lekar(string ime, string prezime, string jmbg, char pol, string brojTelefona, string email, DateTime datumRodenja, string korisnickoIme, string lozinka, AdresaStanovanja adresaStanovanja, TipLekara tipLekara, bool isDeleted = false) : base(ime, prezime, jmbg, pol, brojTelefona, email, datumRodenja, korisnickoIme, lozinka, adresaStanovanja, isDeleted)
    {
        this.tipLekara = tipLekara;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public bool IsAvailable(DateTime pocetak, DateTime kraj) // proverava da li je lekar slobodan izmedju neka dva trenutka u vremenu
    {
        bool retVal = true;
        List<Termin> termini = TerminFileStorage.GetAll();
        foreach (Termin termin in termini)
        {
            if (termin.Lekar.Equals(this) && termin.status == StatusTermina.zakazan)
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
    public static List<string> GetLekarTypes()
    {
        List<string> types = new List<string>();
        types.Add("Opšte Prakse");
        types.Add("Kardiolog");
        types.Add("Hirurg");
        types.Add("Pedijatar");
        types.Add("Neurolog");
        return types;
    }
    public static TipLekara LekarTypeFromString(string type)
    {
        if (type.Equals("Opšte Prakse"))
            return TipLekara.opstePrakse;
        else if (type.Equals("Kardiolog"))
            return TipLekara.kardiolog;
        else if (type.Equals("Hirurg"))
            return TipLekara.hirurg;
        else if (type.Equals("Pedijatar"))
            return TipLekara.pedijatar;
        else if (type.Equals("Opšte Prakse"))
            return TipLekara.neurolog;
        else return 0;
    }

}