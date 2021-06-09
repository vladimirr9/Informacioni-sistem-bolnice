using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.User;
using Newtonsoft.Json;

namespace InformacioniSistemBolnice.FileStorage
{
    class FeedbackRepository
    {
        private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "feedback.json";

        public static List<Feedback> GetAll()
        {
            if (!File.Exists(startupPath))
            {
                var tmp = File.OpenWrite(startupPath);
                tmp.Close();
            }
            List<Feedback> feedbacks;
            String read = File.ReadAllText(startupPath);
            if (read.Equals(""))
            {
                feedbacks = new List<Feedback>();
            }
            else
            {
                feedbacks = JsonConvert.DeserializeObject<List<Feedback>>(read);
            }
            return feedbacks;
        }

        public static Boolean Add(Feedback newfFeedback)
        {
            List<Feedback> feedbacks = GetAll();
            feedbacks.Add(newfFeedback);
            Save(feedbacks);
            return true;
        }

        private static void Save(List<Feedback> feedbacks)
        {
            string write = JsonConvert.SerializeObject(feedbacks);
            File.WriteAllText(startupPath, write);
        }
    }
}
