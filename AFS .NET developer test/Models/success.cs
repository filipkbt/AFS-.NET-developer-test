using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AFS.NET_developer_test.Models
{
    public class success
    {
        [Key, ForeignKey("TranslationId")]
        public int Id { get; set; }

        public virtual TranslationModel TranslationId { get; set; }

        public int total { get; set; }
    }
}