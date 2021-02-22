using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace TestGoogleApi.Models
{
    public class GoogleApi
    {
        public string Title { get; set; }
        public string Snippet { get; set; }
        public string Link { get; set; }
        public string Thumbnail { get; set; }
        public string Query { get; set; }
    }
}