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
            bool threeReviewsOrMore = false;

            if (businesses.Count >= 3)
            {
                threeReviewsOrMore = true;
            }
            return threeReviewsOrMore;
        }
    }
}