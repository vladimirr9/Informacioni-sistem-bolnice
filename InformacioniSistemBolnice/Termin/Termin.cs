// File:    Termin.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:42:32 PM
// Purpose: Definition of Class Termin

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class Termin
{
    public int iDTermina { get; set; }
    public DateTime datumZakazivanja { get; set; }
    public int trajanjeUMinutima { get; set; }
    public TipTermina tipTermina { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public StatusTermina status { get; set; }

    public Pacijent pacijent;

    public Prostorija prostorija;

    public Prostorija Prostorija
    {
        get
        {
            return prostorija;
        }
        set
        {
            this.prostorija = value;
        }
    }
    public Pacijent Pacijent
    {
        get
        {
            return pacijent;
        }
        set
        {
            if (this.pacijent == null || !this.pacijent.Equals(value))
            {
                if (this.pacijent != null)
                {
                    Pacijent oldPacijent = this.pacijent;
                    this.pacijent = null;
                    oldPacijent.RemoveTermin(this);
                }
                if (value != null)
                {
                    this.pacijent = value;
                    this.pacijent.AddTermin(this);
                }
            }
        }
    }
    public Lekar lekar;

    public Termin()
    {
    }

    public Termin(int iDTermina, DateTime datumZakazivanja, int trajanjeUMinutima, TipTermina tipTermina, StatusTermina status, Pacijent pacijent, Lekar lekar, Prostorija prostorija = null)
    {
        this.iDTermina = iDTermina;
        this.datumZakazivanja = datumZakazivanja;
        this.trajanjeUMinutima = trajanjeUMinutima;
        this.tipTermina = tipTermina;
        this.status = status;
        this.prostorija = prostorija;
        Pacijent = pacijent;
        Lekar = lekar;
    }

    public Lekar Lekar
    {
        get
        {
            return lekar;
        }
        set
        {
            this.lekar = value;
        }
    }


}