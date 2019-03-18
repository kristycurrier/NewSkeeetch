using Skeeetch.Data;
using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Skeeetch.Services
{
    public class FindBusinessService
    {
        private readonly SkeeetchContext _context = new SkeeetchContext();

        public string CreateFindBuisnessUrl(SearchTerms searchTerms)
        {
            List<string> searchTermWordList = new List<string>();

            for (int i = 0; i < searchTerms.Terms.Length; i++)
            {
                var iD = searchTerms.Terms[i];
                var dataInput = _context.Categories.First(t => t.ID == iD);
                searchTermWordList.Add(dataInput.SearchTerm);
            }

            var allTerms = string.Join("+", searchTermWordList);
            var url = $"https://api.yelp.com/v3/businesses/search?term={allTerms}&location={searchTerms.City}-{searchTerms.State}&price={searchTerms.Price}";

            return url;
        }

        public async Task<List<Business>> CreateBuisnessListFromApi(string url)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer lXsHa6OCTkq8V1POzIH6RVt09Pv5ClmdHNe7rETSsrMgNNmdOpOGNnxOtLSXBIXEbWXJaq2jU_7_bBi15kUrLMu-Wjb4Xj87-Zotoru48k0JQzZbFc2RcLwQ0BCEXHYx");

            var result = await client.GetAsync(url);
            var businessResults = result.Content.ReadAsAsync<BusinessRoot>();
            List<Business> businessList = businessResults.Result.businesses.ToList();

            return businessList;
        }

        public List<string> GetTopThreeFromSortedList(List<Business> sortedBusinessList)
        {
            List<string> businessListTopThree = new List<string>();

            businessListTopThree.Add(sortedBusinessList.ElementAt(0).YelpId);
            businessListTopThree.Add(sortedBusinessList.ElementAt(1).YelpId);
            businessListTopThree.Add(sortedBusinessList.ElementAt(2).YelpId);

            return businessListTopThree;
        }


    }
}