using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DS.Models
{
    public class Message
    {
        [Key]
        public int MessageId{get;set;}
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Text { get; set; }
    }
}