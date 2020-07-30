using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Xamarin.Forms;
using Xamarin.Essentials;

using Resorg.Entities;

namespace Resorg.Services
{
    /// <summary>
    /// EF Core Database Usage. Please use dictionary-known words.
    /// </summary>
    public class DbDataContext : DbContext
    {
        public DbSet<Resres> Resources { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Category> Categories { get; set; }

        private string _databasePath = "ResResources.db3";

        public DbDataContext(string databasePath=null)
        {
            if (!string.IsNullOrEmpty(databasePath)) 
            {
                _databasePath = databasePath;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                ILocalPath path = DependencyService.Get<ILocalPath>();
                string dbPath = path.DatabasePath(_databasePath);
                if (string.IsNullOrEmpty(dbPath)) throw new Exception("ILocalPath.DatabasePath: returned null reference");

                optionsBuilder.UseSqlite($"Filename={dbPath}");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

    }
}
