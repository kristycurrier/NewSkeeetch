using Newtonsoft.Json;
using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Skeeetch.Services
{
    public class KeywordService
    {
        public IEnumerable<KeyPhrase> CreateKeyPhraseList(List<ReviewRoot> topThreeReviewList)
        {

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

            IEnumerable<KeyPhrase> keyPhraseList = new KeyPhrase[] { firstKeyPhrase, secondKeyPhrase, thirdKeyPhrase };

            return keyPhraseList;
        }

        public async Task<DocumentRoot> GetKeyWordsFromCognitiveServices(IEnumerable<KeyPhrase> keyPhraseList)
        {
            KeyPhraseRoot jsonToSend = new KeyPhraseRoot { Documents = keyPhraseList };

            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "2416a592074c4cde91bf255cb745ddaf");

            var uri = "https://eastus.api.cognitive.microsoft.com/text/analytics/v2.0/keyPhrases?" + queryString;

            HttpResponseMessage response;

            var jsonData = JsonConvert.SerializeObject(jsonToSend);

            using (client)
            {
                response = await client.PostAsync(uri, new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json"));
            }

            var keywords = await response.Content.ReadAsAsync<DocumentRoot>();


            return keywords;
        }

        public DocumentRoot RemoveBusinessName(DocumentRoot keyWords, BusinessRoot listOfBusinesses)
        {
            for (int i = 0; i < listOfBusinesses.businesses.Count(); i++)
            {
                var nameOfBusiness = listOfBusinesses.businesses.ElementAt(0).Name;
                string[] words = nameOfBusiness.Split(' ');

                for (int j = 0; j < keyWords.Documents.Count(); j++)
                {
                    var keyWord = keyWords.Documents.ElementAt(i).KeyPhrases.ElementAt(j);

                    foreach (var word in words)
                    {
                        if (word == keyWord)
                        {
                            keyWords.Documents.ElementAt(i).KeyPhrases.RemoveAt(j);
                        }
                    }
                }
            }
            return keyWords;
        }

    }
}