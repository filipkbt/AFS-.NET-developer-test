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
    }
}