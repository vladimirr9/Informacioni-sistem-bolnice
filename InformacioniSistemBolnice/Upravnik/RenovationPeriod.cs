// File:    RenovationPeriod.cs
// Author:  Tamara
// Created: Monday, May 17, 2021 6:48:14 PM
// Purpose: Definition of Class RenovationPeriod
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RenovationPeriod
{
    private DateTime dateFrom;
    private DateTime dateTo;
    private Boolean isDeleted = false;
    private Prostorija room;

    #region
    public DateTime DateFrom
    {
        get { return dateFrom; }
        set { dateFrom = value; }
    }

    public DateTime DateTo
    {
        get { return dateTo; }
        set { dateTo = value; }
    }

    public Boolean IsDeleted
    {
        get { return isDeleted; }
        set { isDeleted = value; }
    }

    public Prostorija Room
    {
        get { return room; }
        set { room = value; }
    }
    #endregion

    public RenovationPeriod() { }

    public RenovationPeriod(DateTime dateFrom, DateTime dateTo, Boolean isDeleted, Prostorija prostorija)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
        IsDeleted = isDeleted;
        Room = prostorija;
    }
}