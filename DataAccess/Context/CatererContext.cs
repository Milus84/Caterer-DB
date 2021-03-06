﻿using DataAccess.Interfaces;
using DataAccess.Model;
using EntityFramework.DynamicFilters;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using System.Data.Entity;

namespace DataAccess.Context
{
    public class CatererContext : DbContext, ICatererContext
    {
        public CatererContext() : base("CatererConnectionString")
        {
        }

        public virtual DbSet<BenutzerGruppe> BenutzerGruppe { get; set; }
        public virtual DbSet<Recht> Recht { get; set; }
        public virtual DbSet<RechteGruppe> RechteGruppe { get; set; }
        public virtual DbSet<Benutzer> Benutzer { get; set; }
        public virtual DbSet<Sparte> Sparte { get; set; }
        public virtual DbSet<Frage> Frage { get; set; }
        public virtual DbSet<Antwort> Antwort { get; set; }
        public virtual DbSet<Fragebogen> Fragebogen { get; set; }
        public virtual DbSet<Config> Config { get; set; }


        public static CatererContext Create()
        {
            return new CatererContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private static string ConnectionStringErmitteln()
        {
            return ConfigurationManager.ConnectionStrings[1].Name;
        }

        public void DisableAllDynamicFilters()
        {
            this.DisableAllFilters();
        }

        public void EnableAllDynamicFilters()
        {
            this.EnableAllFilters();
        }

        public void DisableDynamicFilter(string name)
        {
            this.DisableFilter(name);
        }

        public void EnableDynamicFilter(string name)
        {
            this.EnableFilter(name);
        }

        public void SetModified(object objekt)
        {
            Entry(objekt).State = EntityState.Modified;
        }

    }
}