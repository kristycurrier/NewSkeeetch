using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Skeeetch.Services
{
    public class ReviewService
    {
        public async Task<List<ReviewRoot>> GetTopThreeReviews (List<string> businessList)
        {
            List<ReviewRoot> reviewListofTopThree = new List<ReviewRoot>();

            for (int i = 0; i < businessList.Count; i++)
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer lXsHa6OCTkq8V1POzIH6RVt09Pv5ClmdHNe7rETSsrMgNNmdOpOGNnxOtLSXBIXEbWXJaq2jU_7_bBi15kUrLMu-Wjb4Xj87-Zotoru48k0JQzZbFc2RcLwQ0BCEXHYx");

                var result = await client.GetAsync($"https://api.yelp.com/v3/businesses/{businessList[i]}/reviews");
                var businessReviews = await result.Content.ReadAsAsync<ReviewRoot>();
                reviewListofTopThree.Add(businessReviews);
            }

            return reviewListofTopThree;
        }
    }
}