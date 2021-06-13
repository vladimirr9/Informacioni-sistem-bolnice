using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InformacioniSistemBolnice.FileStorage
{
    public class GenericUserRepository<Entity> where Entity: global::User
    {
        public string StartupPath { get; set; }

        public List<Entity> GetAll()
        {
            if (!File.Exists(StartupPath))
            {
                var tmp = File.OpenWrite(StartupPath);
                tmp.Close();
            }
            List<Entity> entities;
            String allText = File.ReadAllText(StartupPath);
            if (allText.Equals(""))
            {
                entities = new List<Entity>();
            }
            else
            {
                entities = JsonConvert.DeserializeObject<List<Entity>>(allText);
            }
            return entities;
        }

        public Entity GetOne(string username)
        {
            List<Entity> entities = GetAll();
            foreach (Entity entity in entities)
            {
                if (entity.Username.Equals(username))
                    return entity;
            }
            return null;
        }

        public Boolean Remove(string username)
        {
            List<Entity> entities = GetAll();
            foreach (Entity entity in entities)
            {
                if (entity.Username.Equals(username))
                {
                    entities[entities.IndexOf(entity)].IsDeleted = true;
                    Save(entities);
                    return true;
                }
            }
            return false;
        }

        public Boolean Add(Entity newEntity)
        {
            List<Entity> entities = GetAll();
            entities.Add(newEntity);
            Save(entities);
            return true;
        }

        public Boolean Update(string username, Entity newEntity)
        {
            List<Entity> entities = GetAll();
            foreach (Entity entity in entities)
            {
                if (entity.Username.Equals(username))
                {
                    entities[entities.IndexOf(entity)] = newEntity;
                    Save(entities);
                    return true;
                }
            }
            return false;
        }

        private void Save(List<Entity> doctors)
        {
            string serializeObject = JsonConvert.SerializeObject(doctors);
            File.WriteAllText(StartupPath, serializeObject);
        }
    }
}
