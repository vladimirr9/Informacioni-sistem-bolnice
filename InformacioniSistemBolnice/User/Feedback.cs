using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.User
{
    class Feedback
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String Type { get; set; }
        public String Description { get; set; }

        public Feedback(int id, String username, String type, String description)
        {
            this.Id = id;
            this.Username = username;
            this.Type = type;
            this.Description = description;
        }

    }
}
