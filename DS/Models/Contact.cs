using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DS.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string MapLocation { get; set; }
    }
}