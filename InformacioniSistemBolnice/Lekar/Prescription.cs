// File:    Recept.cs
// Author:  Korisnik
// Created: Monday, April 12, 2021 8:25:35 PM
// Purpose: Definition of Class Recept

using System;

public class Prescription
{
   public Lek Drug { get; set; }
   public DateTime Date { get; set; }

    public Doctor Doctor;

    public Prescription(Lek drug, DateTime date, Doctor doctor)
    {
        this.Drug = drug;
        this.Date = date;
        this.Doctor = doctor;
    }

}