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

namespace Skeeetch.Controllers
{
    public class SearchController : Controller
    {
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromHours(1) };


        public async Task<ActionResult> FindBusinesses(SearchTerms searchTerms)

        {

            var allTerms = string.Join("+", searchTerms.Terms); 

            ViewBag.Title = "Search Results";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer lXsHa6OCTkq8V1POzIH6RVt09Pv5ClmdHNe7rETSsrMgNNmdOpOGNnxOtLSXBIXEbWXJaq2jU_7_bBi15kUrLMu-Wjb4Xj87-Zotoru48k0JQzZbFc2RcLwQ0BCEXHYx");

            //var result = await client.GetAsync("https://api.yelp.com/v3/businesses/search?term=taco&location=detroit&price=1");
            var result = await client.GetAsync($"https://api.yelp.com/v3/businesses/search?term={allTerms}&location={searchTerms.City}-{searchTerms.State}&price={searchTerms.Price}");

            var businessResults = result.Content.ReadAsAsync<BusinessRoot>();

            List<Business> rawBusinessResultsList = businessResults.Result.businesses.ToList();

            BusinessList newBusinessList = new BusinessList();

            var validbusinessList = await newBusinessList.ReturnValidBusinessList(searchTerms, rawBusinessResultsList);

            //var sortedBusniessList = Sorting.SortList(validbusinessList, searchTerms);
            var sortList = new Sorting();
            var sortedBusinessList = sortList.SortList(validbusinessList, searchTerms);

            List<string> businessListTopThree = new List<string>();

            businessListTopThree.Add(validbusinessList.ElementAt(0).YelpId);
            businessListTopThree.Add(validbusinessList.ElementAt(1).YelpId);
            businessListTopThree.Add(validbusinessList.ElementAt(2).YelpId);

            _cache.Set("idList", businessListTopThree, _policy);

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
            _cache.Set("topThreeReviews", reviewListofTopThree, _policy);

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


            var firstReviewSet = topThreeReviewList.ElementAt(0).Reviews.ElementAt(0).Text + topThreeReviewList.ElementAt(0).Reviews.ElementAt(1).Text +
                topThreeReviewList.ElementAt(0).Reviews.ElementAt(2).Text;
            var secondReviewSet = topThreeReviewList.ElementAt(1).Reviews.ElementAt(0).Text + topThreeReviewList.ElementAt(1).Reviews.ElementAt(1).Text +
                topThreeReviewList.ElementAt(1).Reviews.ElementAt(2).Text;
            var thirdReviewSet = topThreeReviewList.ElementAt(2).Reviews.ElementAt(0).Text + topThreeReviewList.ElementAt(2).Reviews.ElementAt(1).Text +

                topThreeReviewList.ElementAt(2).Reviews.ElementAt(2).Text;

            var firstYelpId = topThreeReviewList.ElementAt(0).Reviews.ElementAt(1).YelpId;
            var secondYelpId = topThreeReviewList.ElementAt(1).Reviews.ElementAt(1).YelpId;
            var thirdYelpId = topThreeReviewList.ElementAt(2).Reviews.ElementAt(1).YelpId;

            KeyPhrase firstKeyPhrase = new KeyPhrase("en", firstYelpId, firstReviewSet);
            KeyPhrase secondKeyPhrase = new KeyPhrase("en", secondYelpId, secondReviewSet);
            KeyPhrase thirdKeyPhrase = new KeyPhrase("en", thirdYelpId, thirdReviewSet);

            IEnumerable<KeyPhrase> keyPhraseList = new KeyPhrase[] {firstKeyPhrase, secondKeyPhrase, thirdKeyPhrase };

            KeyPhraseRoot jsonToSend = new KeyPhraseRoot { Documents = keyPhraseList };

            
            
            //var keyPhraseList = new List<KeyPhrase>();
            //keyPhraseList.Add(firstKeyPhrase);
            //keyPhraseList.Add(secondKeyPhrase);
            //keyPhraseList.Add(thirdKeyPhrase);

            //var keyPhrasesToSend = new KeyPhraseRoot
            //{
            //    Documents = keyPhraseList
            //};


            //END OF TEST SET UP**************************************************************************
            //*******************************************************************************************

            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "2416a592074c4cde91bf255cb745ddaf");

            var uri = "https://eastus.api.cognitive.microsoft.com/text/analytics/v2.0/keyPhrases?" + queryString;

            HttpResponseMessage response;

            // Request body

            //firstReviewSet = Regex.Replace(firstReviewSet, @"'", "");
            //secondReviewSet = Regex.Replace(secondReviewSet, @"'", "");
            //thirdReviewSet = Regex.Replace(thirdReviewSet, @"'", "");

            //string myJson = "{'documents': [{'language': 'en','id': '"+$"{firstYelpId}" +"','text': '" + $"{firstReviewSet}" + "'},{'language': 'en','id': '"+$"{secondYelpId}" + "','text': '" + $"{secondReviewSet}" + "'}, {'language': 'en','id': '" + $"{thirdYelpId}" + "','text': '" + $"{thirdReviewSet}" + "'}]}";

            //new Json method******************************************************************************************
            //NEWS TEST METHOD****************************************************************************************

            var jsonData = JsonConvert.SerializeObject(jsonToSend);

            using (client)
            {
                response = await client.PostAsync(uri, new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json"));
            }

            //***********************************************************************************************************
            //**********************************************************************************************************

            //using (client)
            //{
            //    response = await client.PostAsync(uri, new StringContent(myJson, Encoding.UTF8, "application/json"));
            //}

            var keywords = await response.Content.ReadAsAsync<DocumentRoot>();

            _cache.Set("keywordcache", keywords, _policy);

            return View(keywords);

        }

        public ActionResult Results(string id)
        {
            var keywords = _cache.Get("keywordcache") as DocumentRoot;
            //var business = _cache.Get("id") as BusinessRoot;
            return View(keywords);
        }


        // GET: Yelp
        public ActionResult Index()
        {
            return View();
        }

        // GET: Yelp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Yelp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yelp/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Yelp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Yelp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Yelp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Yelp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}