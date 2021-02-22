using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using TestGoogleApi.Models;
using TestGoogleApi.Models.ViewModel;

namespace EnglishLearningTools.Controllers
{
    public class GoogleApiController : Controller
    {
        private SearchViewModel _query = new SearchViewModel();
        // GET: GoogleApi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchTest(SearchViewModel search)
        {
           
                
            return View("Index");
        }
       [HttpPost]
        public ActionResult Search(SearchViewModel searchView)
        {
            try
            {
                const string apiKey = "AIzaSyAwl66f4mrLDP1RryIuLJM3YqQ6R3kQiSg";
                var query = searchView.Query;
                List<string> searchEngineIds = new List<string>()
            {
                "f6a21e8707cccea0a",
                "05c9dc6326534b2a5",
                "5ecc748a53e58c9da",
                "fbdd2e695b28556e7",
                "9f783053f1cf12a28",
                "25db02dd13b1461b3",
                "0fdd307850be11aad",
                "69fe4547d54bb68d3",
                "7171702f6f18bfe38",
                "7ef65375643b1a2a3"
            };
                var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
                var listRequest = customSearchService.Cse.List();

                listRequest.Lr = "lang_en";
                listRequest.ExactTerms = query;
                listRequest.Num = 1;
                CultureInfo culture = new CultureInfo("es-ES", false);
                var pages = new List<GoogleApi>();
                var count = 0;
                foreach (var cx in searchEngineIds)
                {
                    if (count == 5)
                    {
                        break;
                    }
                    listRequest.Cx = cx;
                    if (listRequest.Execute().Items != null)
                    {
                        var paging = listRequest.Execute();
                        if (paging != null && culture.CompareInfo.IndexOf(paging.Items[0].Snippet, query, CompareOptions.IgnoreCase) >= 0)
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                var newSnippet = Regex.Replace(paging.Items[0].Snippet, query, "<b>" + query + "</b>", RegexOptions.IgnoreCase);
                                var page = new GoogleApi()
                                {
                                    Title = paging.Context["title"].ToString(),
                                    Snippet = newSnippet.TrimStart('.'),
                                    Link = paging.Items[0].Link,
                                    Query = query
                                };
                                pages.Add(page);

                            }

                            count++;
                        }
                    }

                }

                _query.GoogleApis = pages;
                return View("New", _query);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error");
            }
           
        }

        public ActionResult New()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}