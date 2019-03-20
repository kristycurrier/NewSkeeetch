using Skeeetch.Data;
using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Skeeetch.Logic
{
    public class BusinessList
    {
        private readonly SkeeetchContext _context = new SkeeetchContext();

        public BusinessList()
        {
        }

        public async Task<List<Business>> ReturnValidBusinessList (SearchTerms searchTerms , List<Business> rawBusinessList)
        {
            List<Business> validBusinessList = new List<Business>();
            List<Business> newBusinessList = new List<Business>();

            if (Validation.ListHasEnoughReviews(rawBusinessList) == false)
            {
                newBusinessList = await SearchWithNewMoneyVariables(searchTerms, rawBusinessList);

                if (Validation.ListHasEnoughReviews(newBusinessList))
                {
                    validBusinessList = newBusinessList;
                    return validBusinessList;
                }
                else
                {
                    return null;
                }
            }

            if (Validation.ListHasEnoughReviews(rawBusinessList))
            {
                validBusinessList = rawBusinessList;
            }
            else
            {
                return null;
            }

            return validBusinessList;
        }

        public async Task<List<Business>> SearchWithNewMoneyVariables(SearchTerms searchTerms, List<Business> businesses)
        {
            var price = "1";
            int priceInt = searchTerms.Price;

            if (searchTerms.Price == 1)
            {
                price = "1,2";
            }
            else if(searchTerms.Price == 2)
            {
                price = "1,2,3";
            }
            else if (searchTerms.Price == 3)
            {
                price = "2,3,4";
            }
            else if (searchTerms.Price == 4)
            {
                price = "3,4";
            }
            else
            {
                price = "1,2,3,4";
            }

            List<string> searchTermWordList = new List<string>();

            for (int i = 0; i < searchTerms.Terms.Length; i++)
            {
                var iD = searchTerms.Terms[i];
                var dataInput = _context.Categories.First(t => t.ID == iD);
                searchTermWordList.Add(dataInput.SearchTerm);
            }

            var allTerms = string.Join("+", searchTermWordList);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer lXsHa6OCTkq8V1POzIH6RVt09Pv5ClmdHNe7rETSsrMgNNmdOpOGNnxOtLSXBIXEbWXJaq2jU_7_bBi15kUrLMu-Wjb4Xj87-Zotoru48k0JQzZbFc2RcLwQ0BCEXHYx");

            var result = await client.GetAsync($"https://api.yelp.com/v3/businesses/search?term={allTerms}&location={searchTerms.City}-{searchTerms.State}&price={price}");
            var businessResults = await result.Content.ReadAsAsync<BusinessRoot>();
            List<Business> newBusinessList = businessResults.businesses.ToList();
            return newBusinessList;
        }


    }
}