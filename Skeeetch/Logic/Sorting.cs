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

        public static List<Business> SortList (List<Business> businesses, SearchTerms searchTerms)
        {
            string level = searchTerms.Adventure;
            switch (level)
            {
                case "boring":
                    //create 
                    break;
                case "fun":
                    break;
                case "exciting":
                    break;
                case "sketch":
                    break;
                default:
                    break;
                }
            //var sortedBusinessList = 
            return businesses;
        }

    }
}