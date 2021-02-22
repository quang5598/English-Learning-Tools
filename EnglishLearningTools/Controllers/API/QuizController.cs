using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EnglishLearningTools.Controllers.API
{
    public class QuizController : ApiController
    {
        public IHttpActionResult GetQuiz(int difficulty, string category)
        {
            var client = new RestClient("https://twinword-word-association-quiz.p.rapidapi.com/type1/?level="+ difficulty +"&area=" + category);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "964b1bd0f3mshc8f8c2fb779f46dp1f3d66jsn917cae346f55");
            request.AddHeader("x-rapidapi-host", "twinword-word-association-quiz.p.rapidapi.com");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                return Ok(content);
            }

            return BadRequest();
        }
    }
}
