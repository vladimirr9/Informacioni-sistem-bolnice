using InformacioniSistemBolnice.Korisnik;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace InformacioniSistemBolnice.FileStorage
{
    public class NotificationFileStorage
    {
        private static string _startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "notifications.json";

        public static List<Notification> GetAll()
        {
            if (!File.Exists(_startupPath))
            {
                var tmp = File.OpenWrite(_startupPath);
                tmp.Close();
            }
            List<Notification> notifications;
            String allText = File.ReadAllText(_startupPath);
            if (allText.Equals(""))
            {
                notifications = new List<Notification>();
            }
            else
            {
                notifications = JsonConvert.DeserializeObject<List<Notification>>(allText);
            }
            return notifications;
        }

        public static Notification GetOne(int id)
        {
            List<Notification> notifications = GetAll();
            foreach (Notification notification in notifications)
            {
                if (notification.ID == id)
                    return notifications[notifications.IndexOf(notification)];
            }
            return null;
        }

        public static Boolean RemoveNotification(int id)
        {
            List<Notification> notifications = GetAll();
            foreach (Notification notification in notifications)
            {
                if (notification.ID == id)
                {
                    notifications[notifications.IndexOf(notification)].IsDeleted = true;
                    Save(notifications);
                    return true;
                }
            }
            return false;
        }

        public static Boolean AddNotification(Notification newNotification)
        {
            List<Notification> notifications = GetAll();
            notifications.Add(newNotification);
            Save(notifications);
            return true;
        }

        public static Boolean UpdateNotification(int id, Notification newNotification)
        {
            List<Notification> notifications = GetAll();
            foreach (Notification notification in notifications)
            {
                if (notification.ID == id)
                {
                    notifications[notifications.IndexOf(notification)] = newNotification;
                    Save(notifications);
                    return true;
                }
            }
            return false;
        }

        private static void Save(List<Notification> notifications)
        {
            string contents = JsonConvert.SerializeObject(notifications);
            File.WriteAllText(_startupPath, contents);
        }

    }
}