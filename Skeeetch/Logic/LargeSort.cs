using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skeeetch.Models;

namespace Skeeetch.Logic
{
    public class LargeSort : ISort
    {
        public List<Business> BoringSort(List<Business> businesses)
        {
            List<Business> sortedList = businesses.OrderByDescending(o => o.Rating).ToList();

            return sortedList;
        }

        public List<Business> FunSort(List<Business> businesses)
        {
            //sort by decending, then find 1/4 of the way through the list
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
            List<Business> sortedList = new List<Business>();
            int i;
            int j;

            if(businesses.Count()%4 == 0)
            {
                i = businesses.Count() / 4;
            }
            else if(businesses.Count() % 4 == 1)
            {
                i = (businesses.Count() - 1) / 4;
            }
            else if(businesses.Count() % 4 == 2)
            {
                i = (businesses.Count() - 2) / 4;
            }
            else
            {
                i = (businesses.Count() + 1) / 4;
            }

            j = i - 1;

            for (int k = i; k < businesses.Count(); k++)
            {
                sortedList.Add(businesses.ElementAt(k));

                if (j >= 0)
                {
                    sortedList.Add(businesses.ElementAt(j));
                }
                j--;
            }
            return sortedList;
        }
    }
}