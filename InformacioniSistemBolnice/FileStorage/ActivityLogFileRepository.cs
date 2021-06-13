using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Patient_ns;

namespace InformacioniSistemBolnice.FileStorage
{
    public class ActivityLogFileRepository : IActivityLogRepository
    {
        private string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "activities.json";

        public List<ActivityLog> GetAll()
        {

            if (!File.Exists(startupPath))
            {
                var tmp = File.OpenWrite(startupPath);
                tmp.Close();
            }
            List<ActivityLog> activities;
            String procitano = File.ReadAllText(startupPath);
            if (procitano.Equals(""))
            {
                activities = new List<ActivityLog>();
            }
            else
            {
                activities = JsonConvert.DeserializeObject<List<ActivityLog>>(procitano);
            }
            return activities;
        }

        public Boolean Add(ActivityLog newActivity)
        {
            List<ActivityLog> activities = GetAll();
            activities.Add(newActivity);
            Save(activities);
            return true;
        }

        private void Save(List<ActivityLog> activities)
        {
            string upis = JsonConvert.SerializeObject(activities);
            File.WriteAllText(startupPath, upis);
        }
    }
}
