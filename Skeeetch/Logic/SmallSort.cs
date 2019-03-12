using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skeeetch.Logic
{
    public class SmallSort : ISort
    {
        public List<Business> BoringSort(List<Business> businesses)
        {
            List<Business> sortedList = businesses.OrderByDescending(o => o.Rating).ToList();

            return sortedList;
        }

        public List<Business> FunSort(List<Business> businesses)
        {
            List<Business> sortedList = businesses.OrderByDescending(o => o.Rating).ToList();

            sortedList = MiddleSort(sortedList);

            return sortedList;
        }

        public List<Business> ExcitingSort(List<Business> businesses)
        {
            List<Business> sortedList = businesses.OrderBy(o => o.Rating).ToList();

            sortedList = MiddleSort(sortedList);

            return sortedList;
        }

        public List<Business> SketchSort(List<Business> businesses)
        {
            List<Business> sortedList = businesses.OrderBy(o => o.Rating).ToList();

            return sortedList;
        }

        public List<Business> MiddleSort(List<Business> businesses)
        {
            List<Business> newBusinessList = new List<Business>();

            if (businesses.Count() == 3)
            {
                newBusinessList.Add(businesses.ElementAt(1));
                newBusinessList.Add(businesses.ElementAt(0));
                newBusinessList.Add(businesses.ElementAt(2));
            }
            else if (businesses.Count() == 4)
            {
                newBusinessList.Add(businesses.ElementAt(1));
                newBusinessList.Add(businesses.ElementAt(0));
                newBusinessList.Add(businesses.ElementAt(2));
                newBusinessList.Add(businesses.ElementAt(3));
            }
            else
            {
                newBusinessList.Add(businesses.ElementAt(2));
                newBusinessList.Add(businesses.ElementAt(1));
                newBusinessList.Add(businesses.ElementAt(3));
                newBusinessList.Add(businesses.ElementAt(0));
                newBusinessList.Add(businesses.ElementAt(4));
            }
            return newBusinessList;
        }
    }
}