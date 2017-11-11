using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DS.POCO
{
    public class ControllerAction
    {
        [Key]
        public int Id { set; get; }
        public string Controller { set; get; }
        public string Action { get; set; }
        public string Attributes { get; set; }
        public string ReturnType { get; set; }
    }
}