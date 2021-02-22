using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestGoogleApi.Models.ViewModel
{
    public class PagesViewModel
    {
        public List<GoogleApi> pages { get; set; }

        public PagesViewModel()
        {
            pages = new List<GoogleApi>();
        }

        
    }
}