using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InformacioniSistemBolnice.FileStorage
{
    public class AnamnesisFileRepository : IAnamnesisRepository
    {
        private string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "anamnesis.json";

        public List<Anamnesis> GetAll()
        {
            if (!File.Exists(startupPath))
            {
                var tmp = File.OpenWrite(startupPath);
                tmp.Close();
            }
            List<Anamnesis> anamneses;
            String procitano = File.ReadAllText(startupPath);
            if (procitano.Equals(""))
            {
                anamneses = new List<Anamnesis>();
            }
            else
            {
                anamneses = JsonConvert.DeserializeObject<List<Anamnesis>>(procitano);
            }
            return anamneses;
        }

        public Boolean Add(Anamnesis newAnamnesis)
        {
            List<Anamnesis> anamneses = GetAll();
            anamneses.Add(newAnamnesis);
            Save(anamneses);
            return true;
        }

        public Boolean Update(int idOfAnamnesis, Anamnesis newAnamnesis)
        {
            List<Anamnesis> anamneses = GetAll();
            foreach (Anamnesis a in anamneses)
            {
                if (a.IdOfAnamnesis.Equals(idOfAnamnesis))
                {
                    anamneses[anamneses.IndexOf(a)] = newAnamnesis;
                    Save(anamneses);
                    return true;
                }
            }
            return false;
        }

        private void Save(List<Anamnesis> lista)
        {
            string upis = JsonConvert.SerializeObject(lista);
            File.WriteAllText(startupPath, upis);
        }
    }
}
