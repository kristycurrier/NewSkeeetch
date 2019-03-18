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
            if(!ModelState.IsValid)
            {
                return RedirectToAction("TryAgain");
            }

            FindBusinessService businessService = new FindBusinessService();
            var url = businessService.CreateFindBuisnessUrl(searchTerms);
            List<Business> rawBusinessResultsList = await businessService.CreateBuisnessListFromApi(url);

            BusinessList newBusinessList = new BusinessList();
            var validBusinessList = await newBusinessList.ReturnValidBusinessList(searchTerms, rawBusinessResultsList);
            if (validBusinessList == null)
            {
                return RedirectToAction("TryAgain");
            }

            var sortList = new Sorting();
            var sortedBusinessList = sortList.SortList(validBusinessList, searchTerms);


            List<string> businessListTopThree = businessService.GetTopThreeFromSortedList(sortedBusinessList);

            _cache.Set("idList", businessListTopThree, _policy);
            _cache.Set("sortedBusinessList", sortedBusinessList, _policy);

            return RedirectToAction("Reviews");      
        }

        public async Task<ActionResult> Reviews()
        {
            List<string> businessList = _cache.Get("idList") as List<string>;

            ReviewService reviewService = new ReviewService();
            List<ReviewRoot> reviewListofTopThree = await reviewService.GetTopThreeReviews(businessList);

            _cache.Set("topThreeReviewList", reviewListofTopThree, _policy);

            return RedirectToAction("Keyword");
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

        public async Task<ActionResult> Business(Document document)
        {
            ViewBag.Title = "Business Info";
            int num = Int32.Parse(document.YelpId);
            List<string> businessList = _cache.Get("idList") as List<string>;

            BusinessService businessService = new BusinessService();
            var business = await businessService.ReturnBusiness(businessList, num);

            return View(business);
        }

        public ActionResult TryAgain()
        {

            return View();
        }
    }
}
