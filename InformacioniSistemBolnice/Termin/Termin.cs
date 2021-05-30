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
    private Doctor _doctor;

    public DateTime KrajTermina { 
    get
        {
            return datumZakazivanja.AddMinutes(trajanjeUMinutima);
        } 
    }


    public Termin(int iDTermina, DateTime datumZakazivanja, int trajanjeUMinutima, TipTermina tipTermina, StatusTermina status, Pacijent pacijent, Doctor doctor, Room prostorija)
    {
        this.iDTermina = iDTermina;
        this.datumZakazivanja = datumZakazivanja;
        this.trajanjeUMinutima = trajanjeUMinutima;
        this.tipTermina = tipTermina;
        this.status = status;
        Pacijent = pacijent;
        Doctor = doctor;
        Prostorija = prostorija;
        IdProstorije = prostorija.RoomId;
        KorisnickoImeLekara = doctor.korisnickoIme;
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
    public Doctor Doctor
    {
        get
        {
            return LekarFileStorage.GetOne(KorisnickoImeLekara);
        }
        set
        {
            if (this._doctor == null || !this._doctor.Equals(value))
            {
                if (this._doctor != null)
                {
                    Doctor oldDoctor = this._doctor;
                    this._doctor = null;
                }
                if (value != null)
                {
                    this._doctor = value;
                    KorisnickoImeLekara = this._doctor.korisnickoIme;
                }
            }
        }
    }
    [JsonIgnore]
    public Room Prostorija
    {
        get
        {
            return RoomFileRepository.GetOne(IdProstorije);
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
    public bool InvolvesEither(Pacijent patient, Doctor doctor)
    {
        if (patient == null && doctor == null)
            return false;
        else if (patient == null)
            return (Doctor.Equals(doctor));
        else if (doctor == null)
            return (Pacijent.Equals(patient));
        else
            return (Doctor.Equals(doctor)) || (Pacijent.Equals(patient));
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
            if (termin.Doctor.Equals(this.Doctor))
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