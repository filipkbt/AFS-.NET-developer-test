using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace AFS.NET_developer_test.Models
{
    public class TranslationModel
    {
        [Key]
        public int Id { get; set; }

        public string Translated { get; set; }

        public string Text { get; set; }

        public string TranslationType { get; set; }
    }
}