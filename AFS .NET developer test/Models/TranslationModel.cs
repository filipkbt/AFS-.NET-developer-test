using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace AFS.NET_developer_test.Models
{
    public class TranslationModel
    {
        [Key]
        public int Id { get; set; }

        public success Success { get; set; }

        public contents Contents { get; set; }

        public error Error { get; set; }

        public string StatusCode { get; set; }

        public bool IsSuccessStatusCode { get; set; }

        public DateTime Date { get; set; }

    }
}