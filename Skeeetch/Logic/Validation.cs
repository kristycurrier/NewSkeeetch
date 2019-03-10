using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skeeetch.Logic
{
    public class Validation
    {
        public static bool ListHasEnoughReviews(List<Business> businesses)
        {
            bool moreThenThreeReviews = false;
            List<Business> listToCheck = new List<Business>();

            for (int i = 0; i < businesses.Count; i++)
            {
                if (businesses.ElementAt(i).ReviewCount >= 3)
                {
                    listToCheck.Add(businesses.ElementAt(i));
                }
            }

            if (listToCheck.Count >= 3)
            {
                moreThenThreeReviews = true;
            }
            return moreThenThreeReviews;
        }

    }
}