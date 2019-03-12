using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skeeetch.Models
{
    public class KeyPhrase
    {
        public string Language { get; set; }
        [JsonProperty("id")]
        public string YelpId { get; set; }
        public string Text { get; set; }
    }
}