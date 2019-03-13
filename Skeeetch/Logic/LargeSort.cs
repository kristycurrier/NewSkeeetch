using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skeeetch.Models;

namespace Skeeetch.Logic
{
    public class LargeSort : SortBase
    {
        public override List<Business> MiddleSort(List<Business> businesses)
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