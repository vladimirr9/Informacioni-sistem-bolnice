using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Korisnik
{

    public class Notification
    {
        public int ID { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public DateTime CreationDate { get; set; }
        public Boolean IsRead { get; set; }
        public Boolean IsDeleted { get; set; }
        public List<string> Recipients { get; set; }   // ALL_USERS, EMPLOYED_USERS, PATIENT_USERS or username as values
        public Notification(int id, string title, string content, DateTime creationDate, bool isRead = false, bool isDeleted = false)
        {
            this.ID = id;
            this.Title = title;
            this.Content = content;
            this.CreationDate = creationDate;
            this.IsRead = isRead;
            this.IsDeleted = isDeleted;
            Recipients = new List<string>();
        }


        [JsonIgnore]
        public String Header
        {
            get
            {
                if (Recipients.Contains("ALL_USERS"))
                    return CreationDate.ToString("dd/MM/yyyy HH:mm  - ") + Title + " GLOBAL";
                else
                    return CreationDate.ToString("dd/MM/yyyy HH:mm  - ") + Title;
            }   
        }

        public bool IsDirectedTo(string username)
        {
            if (Recipients.Contains("ALL_USERS"))
                return true;
            if (PacijentFileStorage.GetOne(username) != null)
            {
                if (Recipients.Contains("PATIENT_USERS") || Recipients.Contains(username))
                    return true;
            }
            else if (SecretaryFileStorage.GetOne(username) != null || LekarFileStorage.GetOne(username) != null || UpravnikFileStorage.GetOne(username) != null)
            {
                if (Recipients.Contains("EMPLOYED_USERS") || Recipients.Contains(username))
                    return true;
            }
            return false;
        }

        public void FillRecipients(List<string> recipients)
        {
            foreach (var item in recipients)
            {
                if (item.Equals("Svi korisnici"))
                    Recipients.Add("ALL_USERS");
                else if (item.Equals("Zaposleni"))
                    Recipients.Add("EMPLOYED_USERS");
                else if (item.Equals("Svi pacijenti"))
                    Recipients.Add("PATIENT_USERS");
                else
                {
                    String username = item.ToString().Split('-')[1].Trim();
                    Recipients.Add(username);
                }
            }
        }


    }
}
