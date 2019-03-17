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

        public bool SearchTermsIsValid(SearchTerms searchTerms)
        {
            bool valid = false;

            //if (searchTerms.Adventure.Count()>0 && searchTerms.City.Count()>1 && searchTerms.Price.//

            return valid;
        }
    }
}