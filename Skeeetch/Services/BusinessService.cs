using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Skeeetch.Services
{
    public class BusinessService
    {

        public async Task<Business> ReturnBusiness ( List<string> businessList ,int num)
        {
            var id = businessList[num];
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer lXsHa6OCTkq8V1POzIH6RVt09Pv5ClmdHNe7rETSsrMgNNmdOpOGNnxOtLSXBIXEbWXJaq2jU_7_bBi15kUrLMu-Wjb4Xj87-Zotoru48k0JQzZbFc2RcLwQ0BCEXHYx");
            var result = client.GetAsync($"https://api.yelp.com/v3/businesses/{id}").Result;
            var business = result.Content.ReadAsAsync<Business>().Result;

            return business;
        }

    }
}