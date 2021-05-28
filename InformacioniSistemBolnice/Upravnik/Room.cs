// File:    Prostorija.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:44:09 PM
// Purpose: Definition of Class Prostorija

using System;
using System.Collections.Generic;

public class Room
{
   private String _name;
   private int _roomId;
   private RoomType _roomType;
   private Boolean _isDeleted = false;
   private Boolean _isActive;
   private Double _area;
   private int _floorNumber;
   private int _roomNumber;
   private List<Inventory> _inventoryList;

    #region Properties
    public String Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public int RoomId
    {
        get { return _roomId; }
        set { _roomId = value; }
    }
    public RoomType RoomType
    {
        get { return _roomType; }
        set { _roomType = value; }
    }
    public Boolean IsDeleted
    {
        get { return _isDeleted; }
        set { _isDeleted = value; }
    }
    public Boolean IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }
    public Double Area
    {
        get { return _area; }
        set { _area = value; }
    }
    public int FloorNumber
    {
        get { return _floorNumber; }
        set { _floorNumber = value; }
    }
    public int RoomNumber
    {
        get { return _roomNumber; }
        set { _roomNumber = value; }
    }

    public List<Inventory> InventoryList
    {
        get { return _inventoryList; }
        set { _inventoryList = value; }
    }
    #endregion

    public Inventory GetOne(string SifraOpreme)
    {
        foreach (Inventory o in _inventoryList)
        {
            if (o.Id.Equals(SifraOpreme))
            {
                return _inventoryList[_inventoryList.IndexOf(o)];
            }
        }
        return null;
    }

    public Room() { }

    public Room(String name, int roomId, RoomType roomType, Boolean isDeleted, Boolean isActive, Double area, int floorNumber, int roomNumber, List<Inventory> inventoryList)
    {
        Name = name;
        RoomId = roomId;
        RoomType = roomType;
        IsDeleted = isDeleted;
        IsActive = isActive;
        Area = area;
        FloorNumber = floorNumber;
        RoomNumber = roomNumber;
        InventoryList = inventoryList;
    }

    /*public Prostorija(String Name, int iDprostorije, TipProstorije tipProstorije, Boolean IsDeleted, Boolean isActive, Double kvadratura, int brSprata, int brSobe)
    {
        Naziv = Name;
        IDprostorije = iDprostorije;
        TipProstorije = tipProstorije;
        this.IsDeleted = false;
        IsActive = isActive;
        Kvadratura = kvadratura;
        BrSprata = brSprata;
        BrSobe = brSobe;
    }*/


    public bool IsAvailable(DateTime startDate, DateTime endDate) // proverava da li je RoomComboBox slobodna izmedju neka dva trenutka u vremenu
    {
        if (startDate.Equals(endDate))
            return true;
        bool retVal = true;
        List<Termin> termini = TerminFileStorage.GetAll();
        foreach (Termin termin in termini)
        {
            if (termin.Prostorija.Equals(this) && termin.status == StatusTermina.zakazan)
            {
                if (startDate >= termin.datumZakazivanja && startDate <= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (endDate >= termin.datumZakazivanja && endDate <= termin.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (startDate <= termin.datumZakazivanja && endDate >= termin.KrajTermina)
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
        return obj is Room room &&
               RoomId == room.RoomId;
    }
}