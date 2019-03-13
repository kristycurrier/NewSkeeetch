using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skeeetch.Logic
{
    public abstract class SortBase
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

        public abstract List<Business> MiddleSort(List<Business> businesses);
    }
}