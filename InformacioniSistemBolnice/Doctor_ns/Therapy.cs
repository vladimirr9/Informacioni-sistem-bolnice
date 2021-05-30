// File:    Terapija.cs
// Author:  User
// Created: Monday, April 12, 2021 8:15:39 PM
// Purpose: Definition of Class Terapija

using System;

public class Therapy
{
   public String Description { get; set; }
   public DateTime BeginningDate { get; set; }
   public DateTime EndingDate { get; set; }
   public int Day { get; set; }

    public Therapy()
    {
    }

    public Therapy(string description, DateTime begin, DateTime end, int day)
    {
        Description = description;
        BeginningDate = begin;
        EndingDate = end;
        Day = day;
    }
}