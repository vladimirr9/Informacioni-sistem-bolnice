﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Doctor_ns;
using Newtonsoft.Json;

namespace InformacioniSistemBolnice.FileStorage
{
    class HospitalisationFileRepository : IHospitalisationRepository
    {
        private string _startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "hospitalisations.json";
        public List<Hospitalisation> GetAll()
        {
            if (!File.Exists(_startupPath))
            {
                var tmp = File.OpenWrite(_startupPath);
                tmp.Close();
            }
            List<Hospitalisation> hospitalisations;
            String allText = File.ReadAllText(_startupPath);
            if (allText.Equals(""))
            {
                hospitalisations = new List<Hospitalisation>();
            }
            else
            {
                hospitalisations = JsonConvert.DeserializeObject<List<Hospitalisation>>(allText);
            }
            return hospitalisations;
        }

        public Hospitalisation GetOne(int hospitalisationId)
        {
            List<Hospitalisation> hospitalisations = GetAll();
            foreach (Hospitalisation hospitalisation in hospitalisations)
            {
                if (hospitalisation.HospitalisationId.Equals(hospitalisationId))
                    return hospitalisation;
            }
            return null;
        }

        public Boolean Remove(int hospitalisationId)
        {
            List<Hospitalisation> hospitalisations = GetAll();
            foreach (Hospitalisation hospitalisation in hospitalisations)
            {
                if (hospitalisation.HospitalisationId.Equals(hospitalisationId))
                {
                    hospitalisations[hospitalisations.IndexOf(hospitalisation)].IsDeleted = true;
                    Save(hospitalisations);
                    return true;
                }
            }
            return false;
        }

        private void Save(List<Hospitalisation> hospitalisations)
        {
            string serializeObject = JsonConvert.SerializeObject(hospitalisations, Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            File.WriteAllText(_startupPath, serializeObject);
        }

        public Boolean Add(Hospitalisation newHospitalisation)
        {
            List<Hospitalisation> hospitalisations = GetAll();
            hospitalisations.Add(newHospitalisation);
            Save(hospitalisations);
            return true;
        }

        public Boolean Update(int hospitalisationId, Hospitalisation newHospitalisation)
        {
            List<Hospitalisation> hospitalisations = GetAll();
            foreach (Hospitalisation hospitalisation in hospitalisations)
            {
                if (hospitalisation.HospitalisationId.Equals(hospitalisationId))
                {
                    hospitalisations[hospitalisations.IndexOf(hospitalisation)] = newHospitalisation;
                    Save(hospitalisations);
                    return true;
                }
            }
            return false;
        }
    }
}
