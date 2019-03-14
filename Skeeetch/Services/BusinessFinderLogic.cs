using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Skeeetch.ControllerLogic
{
    public class BusinessFinderLogic 
    {
        public string JoinSerchTerms(SearchTerms searchTerms)
        {
            var allTerms = string.Join("+", searchTerms.Terms);
            return allTerms;
        }

        public List<Business> CreateBusinessList(HttpResponseMessage result)
        {
            var businessResults = result.Content.ReadAsAsync<BusinessRoot>();

            List<Business> rawBusinessResultsList = businessResults.Result.businesses.ToList();
            return rawBusinessResultsList;
        }


        
        BusinessList newBusinessList = new BusinessList();

        var validbusinessList = await newBusinessList.ReturnValidBusinessList(searchTerms, rawBusinessResultsList);

        List<string> businessListTopThree = new List<string>();

        businessListTopThree.Add(validbusinessList.ElementAt(0).YelpId);
            businessListTopThree.Add(validbusinessList.ElementAt(1).YelpId);
            businessListTopThree.Add(validbusinessList.ElementAt(2).YelpId);

        


    }
}