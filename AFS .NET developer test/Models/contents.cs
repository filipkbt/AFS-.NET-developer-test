using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AFS.NET_developer_test.Models
{
    public class contents
    {
        [Key, ForeignKey("TranslationId")]
        public int Id { get; set; }

        public virtual TranslationModel TranslationId { get; set; }
        
        public string translated { get; set; }

        [Required]
        public string text { get; set; }

        [Required]
        public string translation { get; set; }
    }
}