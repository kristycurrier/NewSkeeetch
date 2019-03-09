using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skeeetch.Models
{
    public class SearchTerms
    {
        public string[] Terms { get; set; }
        public int Price { get; set; }
        public string Adventure { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}