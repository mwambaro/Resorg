using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;
using Resorg.Entities;

namespace Resorg.Services
{
    public class DbDataContext : DbContext
    {
        public DbSet<Resres> Resreses { get; set; }
        public DbSet<Note> Notes { get; set; }
        private string _databasePath = "resresources.db";

        public DbDataContext(string databasePath=null)
        {
            if (!string.IsNullOrEmpty(databasePath)) 
            {
                _databasePath = databasePath;
            }
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, _databasePath);
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }

    }
}
