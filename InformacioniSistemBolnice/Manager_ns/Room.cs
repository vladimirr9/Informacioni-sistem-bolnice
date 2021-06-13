// File:    Room.cs
// Author:  User
// Created: Monday, March 22, 2021 6:44:09 PM
// Purpose: Definition of Class Room

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
   private int _floor;
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
    public int Floor
    {
        get { return _floor; }
        set { _floor = value; }
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
            if (o.InventoryId.Equals(SifraOpreme))
            {
                return _inventoryList[_inventoryList.IndexOf(o)];
            }
        }
        return null;
    }

    public Room() { }

    public Room(String name, int id, RoomType type, Boolean isDeleted, Boolean isActive, Double area, int floor, int roomNumber, List<Inventory> inventoryList)
    {
        Name = name;
        RoomId = id;
        RoomType = type;
        IsDeleted = isDeleted;
        IsActive = isActive;
        Area = area;
        Floor = floor;
        RoomNumber = roomNumber;
        InventoryList = inventoryList;
    }

    /*public Room(String Name, int iDprostorije, TipProstorije tipProstorije, Boolean IsDeleted, Boolean isActive, Double kvadratura, int brSprata, int brSobe)
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


    public bool IsAvailable(DateTime start, DateTime end) // proverava da li je RoomComboBox slobodna izmedju neka dva trenutka u vremenu
    {
        if (start.Equals(end))
            return true;
        bool retVal = true;
        AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
        List<Appointment> termini = appointmentFileRepository.GetAll();
        foreach (Appointment termin in termini)
        {
            if (termin.Room.Equals(this) && termin.AppointmentStatus == AppointmentStatus.scheduled)
            {
                if (start >= termin.AppointmentDate && start <= termin.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (end >= termin.AppointmentDate && end <= termin.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (start <= termin.AppointmentDate && end >= termin.AppointmentEnd)
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