using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skeeetch.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string DisplayTerm { get; set; }
        public string SearchTerm { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}