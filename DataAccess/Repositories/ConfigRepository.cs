﻿using DataAccess.Interfaces;
using System.Linq;
using DataAccess.Model;

namespace DataAccess.Repositories
{
    public class ConfigRepository : IConfigRepository
    {

        protected ICatererContext Db { get; }

        public ConfigRepository(ICatererContext db)
        {
            Db = db;
        }

        public void AddConfig(Config config)
        {
            Db.Config.Add(config);
            Db.SaveChanges();
        }

        public void EditConfig(Config config)
        {
            Db.SetModified(config);
            Db.SaveChanges();
        }

        public Config GetConfig()
        {
            return Db.Config.Where(x => x.ConfigId == 1).Single(); ;
        }

    }
}
