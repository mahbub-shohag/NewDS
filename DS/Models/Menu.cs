using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DS.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string Caption { get; set; }
        public int ParentId { get; set; }
        public int DisplayPosition { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}