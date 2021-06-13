// File:    AppointmentFileRepository.cs
// Author:  User
// Created: Monday, March 22, 2021 8:27:36 PM
// Purpose: Definition of Class AppointmentFileRepository

using System;
using System.Collections.Generic;
using System.IO;
using InformacioniSistemBolnice.FileStorage;
using Newtonsoft.Json;

public class AppointmentFileRepository : IAppointmentRepository
{
    private string _startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "appointments.json";
    public List<Appointment> GetAll()
    {
        if (!File.Exists(_startupPath))
        {
            var tmp = File.OpenWrite(_startupPath);
            tmp.Close();
        }
        List<Appointment> appointments;
        String allText = File.ReadAllText(_startupPath);
        if (allText.Equals(""))
        {
            appointments = new List<Appointment>();
        }
        else
        {
            appointments = JsonConvert.DeserializeObject<List<Appointment>>(allText);
        }
        return appointments;
    }

    public Appointment GetOne(int appointmentID)
    {
        List<Appointment> appointments = GetAll();
        foreach (Appointment appointment in appointments)
        {
            if (appointment.AppointmentID.Equals(appointmentID))
                return appointment;
        }
        return null;
    }

    public Boolean Remove(int appointmentID)
    {
        List<Appointment> appointments = GetAll();
        foreach (Appointment appointment in appointments)
        {
            if (appointment.AppointmentID.Equals(appointmentID))
            {
                appointments[appointments.IndexOf(appointment)].AppointmentStatus = AppointmentStatus.cancelled;
                Save(appointments);
                return true;
            }
        }
        return false;
    }

    private void Save(List<Appointment> appointments)
    {
        string serializeObject = JsonConvert.SerializeObject(appointments, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
        File.WriteAllText(_startupPath, serializeObject);
    }

    public Boolean Add(Appointment newAppointment)
    {
        List<Appointment> appointments = GetAll();
        appointments.Add(newAppointment);
        Save(appointments);
        return true;
    }

    public Boolean Update(int appointmentID, Appointment newAppointment)
    {
        List<Appointment> appointments = GetAll();
        foreach (Appointment appointment in appointments)
        {
            if (appointment.AppointmentID.Equals(appointmentID))
            {
                appointments[appointments.IndexOf(appointment)] = newAppointment;
                Save(appointments);
                return true;
            }
        }
        return false;
    }
}