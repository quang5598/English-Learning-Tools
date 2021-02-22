using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestGoogleApi.Models.ViewModel
{
    public class SearchViewModel
    {
        public string Query { get; set; }
        public List<GoogleApi> GoogleApis { get; set; }

        public SearchViewModel()
        {
            GoogleApis = new List<GoogleApi>();
        }
        
    }
}