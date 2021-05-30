// File:    ApointmentFileRepository.cs
// Author:  User
// Created: Monday, March 22, 2021 8:27:36 PM
// Purpose: Definition of Class ApointmentFileRepository

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class ApointmentFileRepository
{
    private static string _startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "appointments.json";
    public static List<Appointment> GetAll()
    {
        if (!File.Exists(_startupPath))
        {
            var tmp = File.OpenWrite(_startupPath);
            tmp.Close();
        }
        List<Appointment> appointmnets;
        String allText = File.ReadAllText(_startupPath);
        if (allText.Equals(""))
        {
            appointmnets = new List<Appointment>();
        }
        else
        {
            appointmnets = JsonConvert.DeserializeObject<List<Appointment>>(allText);
        }
        return appointmnets;
    }

    public static Appointment GetOne(int appointmentID)
    {
        List<Appointment> appointments = GetAll();
        foreach (Appointment appointment in appointments)
        {
            if (appointment.AppointmentID.Equals(appointmentID))
                return appointment;
        }
        return null;
    }

    public static Boolean RemoveAppointment(int appointmentID)
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

    private static void Save(List<Appointment> appointments)
    {
        string serializeObject = JsonConvert.SerializeObject(appointments, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
        File.WriteAllText(_startupPath, serializeObject);
    }

    public static Boolean AddAppointment(Appointment newAppointment)
    {
        List<Appointment> appointments = GetAll();
        appointments.Add(newAppointment);
        Save(appointments);
        return true;

    }

    public static Boolean UpdateAppointment(int appointmentID, Appointment newAppointment)
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