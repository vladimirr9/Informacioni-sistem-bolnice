using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public class DatumiProvjereStatusaFileStorage
    {
        private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "datumi.json";

        public static List<DateTime> GetAll()
        {

            if (!File.Exists(startupPath))
            {
                var tmp = File.OpenWrite(startupPath);
                tmp.Close();
            }
            List<DateTime> datumi;
            String procitano = File.ReadAllText(startupPath);
            if (procitano.Equals(""))
            {
                datumi = new List<DateTime>();
            }
            else
            {
                datumi = JsonConvert.DeserializeObject<List<DateTime>>(procitano);
            }
            return datumi;

        }


        public static void AddDatum(DateTime datum)
        {
            List<DateTime> datumi = GetAll();
            datumi.Add(datum);
            Save(datumi);
            
        }

        public static void Save(List<DateTime> datumi)
        {
            string upis = JsonConvert.SerializeObject(datumi);
            File.WriteAllText(startupPath, upis);
        }

        public static DateTime PosljednjiDatumProvjere() {
            List<DateTime> datumi = GetAll();
            if (datumi.Count > 0) {
                DateTime datumProvjere = datumi.ElementAt(0);
                foreach (DateTime datum in datumi) {
                    if (datum> datumProvjere) {
                        datumProvjere = datum;
                        
                    }
                    
                }

                return datumProvjere;
            }
            return DateTime.Parse("1970-01-01T00:00:00");
        }
    }
    }
