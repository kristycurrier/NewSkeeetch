using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skeeetch.Models;

namespace Skeeetch.Logic
{
    public class MediumSort : ISort
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
            List<Business> sortedList = new List<Business>();
            int i;
            int j;

            if(businesses.Count() == 6 || businesses.Count()==7 || businesses.Count() ==8) 
            {
                i = 2;
                j = 1;
            }
            else
            {
                i = 3;
                j = 2;
            }

            for (int k = i; k < businesses.Count(); k++)
            {
                sortedList.Add(businesses.ElementAt(k));

                if (j>=0)
                {
                    sortedList.Add(businesses.ElementAt(j));
                }
                j--;
            }

            return sortedList;
        }
    }
}