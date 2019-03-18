using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skeeetch.Logic
{
    public class Sorting
    {
        //make method to determine sketch level then send it to the size of sort class methods

        public List<Business> SortList (List<Business> businesses, SearchTerms searchTerms)
        {

            var factory = new SortFactory();
            var sort = factory.Create(businesses.Count);

            string level = searchTerms.Adventure;
            var results = new List<Business>();

            switch (level)
            {
                case "boring":
                    results = sort.BoringSort(businesses);
                    break;
                case "fun":
                    results = sort.FunSort(businesses);
                    break;
                case "exciting":
                    results = sort.ExcitingSort(businesses);
                    break;
                case "sketch":
                    results = sort.SketchSort(businesses);
                    break;
                default:
                    results = null;
                    break;
                }

            return results;
        }

    }
}