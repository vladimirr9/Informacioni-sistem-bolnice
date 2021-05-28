// File:    Termin.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:42:32 PM
// Purpose: Definition of Class Termin

using System;
using System.Collections.Generic;
using InformacioniSistemBolnice.Secretary_ns;
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

    [JsonIgnore]
    private Pacijent pacijent;
    [JsonIgnore]
    private Room prostorija;

    public String anamneza { get; set; }


    public int IdProstorije;
    public string KorisnickoImeLekara;
    public string KorisnickoImePacijenta;

    [JsonIgnore]
    public Pacijent Pacijent
    {
        get
        {
            return PacijentFileStorage.GetOne(KorisnickoImePacijenta);
        }
        set
        {
            if (this.pacijent == null || !this.pacijent.Equals(value))
            {
                if (this.pacijent != null)
                {
                    Pacijent oldPacijent = this.pacijent;
                    this.pacijent = null;
                }
                if (value != null)
                {
                    this.pacijent = value;
                    KorisnickoImePacijenta = this.pacijent.korisnickoIme;
                }
            }
        }
    }
    [JsonIgnore]
    private Lekar lekar;

    public DateTime KrajTermina { 
    get
        {
            return datumZakazivanja.AddMinutes(trajanjeUMinutima);
        } 
    }


    public Termin(int iDTermina, DateTime datumZakazivanja, int trajanjeUMinutima, TipTermina tipTermina, StatusTermina status, Pacijent pacijent, Lekar lekar, Room prostorija)
    {
        this.iDTermina = iDTermina;
        this.datumZakazivanja = datumZakazivanja;
        this.trajanjeUMinutima = trajanjeUMinutima;
        this.tipTermina = tipTermina;
        this.status = status;
        Pacijent = pacijent;
        Lekar = lekar;
        Prostorija = prostorija;
        IdProstorije = prostorija.RoomId;
        KorisnickoImeLekara = lekar.korisnickoIme;
        KorisnickoImePacijenta = pacijent.korisnickoIme;
    }

    [JsonConstructor]
    public Termin(int iDTermina, DateTime datumZakazivanja, int trajanjeUMinutima, TipTermina tipTermina, StatusTermina status,  int IdProstorije, string KorisnickoImeLekara, string KorisnickoImePacijenta)
    {
        this.iDTermina = iDTermina;
        this.datumZakazivanja = datumZakazivanja;
        this.trajanjeUMinutima = trajanjeUMinutima;
        this.tipTermina = tipTermina;
        this.status = status;
        this.IdProstorije = IdProstorije;
        this.KorisnickoImeLekara = KorisnickoImeLekara;
        this.KorisnickoImePacijenta = KorisnickoImePacijenta;
    }



    [JsonIgnore]
    public Lekar Lekar
    {
        get
        {
            return LekarFileStorage.GetOne(KorisnickoImeLekara);
        }
        set
        {
            if (this.lekar == null || !this.lekar.Equals(value))
            {
                if (this.lekar != null)
                {
                    Lekar oldLekar = this.lekar;
                    this.lekar = null;
                }
                if (value != null)
                {
                    this.lekar = value;
                    KorisnickoImeLekara = this.lekar.korisnickoIme;
                }
            }
        }
    }
    [JsonIgnore]
    public Room Prostorija
    {
        get
        {
            return RoomFileRepoistory.GetOne(IdProstorije);
        }
        set
        {
            if (this.prostorija == null || !this.prostorija.Equals(value))
            {
                if (this.prostorija != null)
                {
                    Room oldProstorija = this.prostorija;
                    this.prostorija = null;
                }
                if (value != null)
                {
                    this.prostorija = value;
                    IdProstorije = this.prostorija.RoomId;
                }
            }
        }
    }
    [JsonIgnore]
    public int PostponementDuration { get; set; }
    public static int SortByPostponementDurationAscending(Termin x, Termin y)
    {

        return x.PostponementDuration.CompareTo(y.PostponementDuration);
    }


    public bool OccursOn(DateTime date)
    {
        return datumZakazivanja.Date.Equals(date.Date);
    }
    public bool InvolvesEither(Pacijent patient, Lekar doctor)
    {
        if (patient == null && doctor == null)
            return false;
        else if (patient == null)
            return (Lekar.Equals(doctor));
        else if (doctor == null)
            return (Pacijent.Equals(patient));
        else
            return (Lekar.Equals(doctor)) || (Pacijent.Equals(patient));
    }
    public bool AreAllEntitiesAvailable(List<Termin> appointmentsToCheck = null)
    {

        bool retVal = true;
        List<Termin> termini;
        if (appointmentsToCheck == null)
            termini = TerminFileStorage.GetAll();
        else
            termini = appointmentsToCheck;
        foreach (Termin termin in termini)
        {
            if (termin.status != StatusTermina.zakazan)
                continue;
            if (termin.Pacijent.Equals(this.Pacijent))
            {
                if (this.datumZakazivanja >= termin.datumZakazivanja && this.datumZakazivanja <= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (this.KrajTermina >= termin.datumZakazivanja && this.KrajTermina <= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (this.datumZakazivanja <= termin.datumZakazivanja && this.KrajTermina >= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
            }
            if (termin.Lekar.Equals(this.Lekar))
            {
                if (this.datumZakazivanja >= termin.datumZakazivanja && this.datumZakazivanja <= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (this.KrajTermina >= termin.datumZakazivanja && this.KrajTermina <= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (this.datumZakazivanja <= termin.datumZakazivanja && this.KrajTermina >= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
            }
            if (termin.Prostorija.Equals(this.Prostorija))
            {
                if (this.datumZakazivanja >= termin.datumZakazivanja && this.datumZakazivanja <= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (this.KrajTermina >= termin.datumZakazivanja && this.KrajTermina <= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (this.datumZakazivanja <= termin.datumZakazivanja && this.KrajTermina >= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
            }
            
        }
        return retVal;
    }
}