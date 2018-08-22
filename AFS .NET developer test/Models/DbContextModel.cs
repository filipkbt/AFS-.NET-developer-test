using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AFS.NET_developer_test.Models
{
    public class DbContextModel : DbContext
    {
        public DbSet<TranslationModel> Translations { get; set; }

        public DbSet<error> Errors { get; set; }

        public DbSet<contents> Contents { get; set; }

        public DbSet<success> Successes { get; set; }
    }
}