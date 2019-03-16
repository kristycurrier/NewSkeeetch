using Skeeetch.Data;
using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using Skeeetch.Logic;
using Newtonsoft.Json;
using Skeeetch.Services;

namespace Skeeetch.Controllers
{
    public class SearchController : Controller
    {
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromHours(1) };

        private readonly SkeeetchContext _context = new SkeeetchContext();


        public async Task<ActionResult> FindBusinesses(SearchTerms searchTerms)
        {
            var url = CreateFindBuisnessUrl(searchTerms);

            List<Business> rawBusinessResultsList = await CreateBuisnessListFromApi(url);

            BusinessList newBusinessList = new BusinessList();

            var validBusinessList = await newBusinessList.ReturnValidBusinessList(searchTerms, rawBusinessResultsList);

            var sortList = new Sorting();
            var sortedBusinessList = sortList.SortList(validBusinessList, searchTerms);

            List<string> businessListTopThree = GetTopThreeFromSortedList(sortedBusinessList);

            _cache.Set("idList", businessListTopThree, _policy);
            _cache.Set("sortedBusinessList", sortedBusinessList, _policy);

            return RedirectToAction("Reviews");

      
        }


        public async Task<ActionResult> Reviews()
        {
            List<string> businessList = _cache.Get("idList") as List<string>;
            List<ReviewRoot> reviewListofTopThree = new List<ReviewRoot>();

            for (int i = 0; i < businessList.Count; i++)
            {
                ViewBag.Title = "Reviews";
                var client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer lXsHa6OCTkq8V1POzIH6RVt09Pv5ClmdHNe7rETSsrMgNNmdOpOGNnxOtLSXBIXEbWXJaq2jU_7_bBi15kUrLMu-Wjb4Xj87-Zotoru48k0JQzZbFc2RcLwQ0BCEXHYx");

                var result = await client.GetAsync($"https://api.yelp.com/v3/businesses/{businessList[i]}/reviews");
                var businessReviews = await result.Content.ReadAsAsync<ReviewRoot>();
                reviewListofTopThree.Add(businessReviews);
            }

            _cache.Set("topThreeReviewList", reviewListofTopThree, _policy);

            return RedirectToAction("Keyword");
        }


        public ActionResult Business(Document document)
        {
            int num = Int32.Parse(document.YelpId);
            List<string> businessList = _cache.Get("idList") as List<string>;
            var id = businessList[num];
            ViewBag.Title = "Business Info";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer lXsHa6OCTkq8V1POzIH6RVt09Pv5ClmdHNe7rETSsrMgNNmdOpOGNnxOtLSXBIXEbWXJaq2jU_7_bBi15kUrLMu-Wjb4Xj87-Zotoru48k0JQzZbFc2RcLwQ0BCEXHYx");
            var result = client.GetAsync($"https://api.yelp.com/v3/businesses/{id}").Result;
            var business = result.Content.ReadAsAsync<Business>().Result;

            return View(business);
        }

        public async Task<ActionResult> Keyword()
        {
            var topThreeReviewList = _cache.Get("topThreeReviewList") as List<ReviewRoot>;
            var sortedList = _cache.Get("sortedBusinessList") as List<Business>;

            KeywordService service = new KeywordService();
            IEnumerable<KeyPhrase> keyPhraseList = service.CreateKeyPhraseList(topThreeReviewList);
            var keywords = await service.GetKeyWordsFromCognitiveServices(keyPhraseList);
            keywords = service.RemoveBusinessName(keywords, sortedList);

            _cache.Set("keywordcache", keywords, _policy);

            return View(keywords);

        }


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
