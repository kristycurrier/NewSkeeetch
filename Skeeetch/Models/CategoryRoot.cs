using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skeeetch
{
    public class CategoryRoot
    {
        public virtual ICollection<Category> Categories { get; set; }
    }
}