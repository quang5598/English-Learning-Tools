using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnglishLearningTools.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EnglishLearningTools.Controllers.API
{
    public class DictionaryController : ApiController
    {
        
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> UserManager { get; set; }

        public DictionaryController()
        {
            _context = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

        }

        public async Task<IHttpActionResult> GetWord(string word)
        {
            var client = new RestClient($"https://api.dictionaryapi.dev/api/v2/entries/en/{word}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                return Ok(content);
            }

            return BadRequest();
        }

        [HttpPost]
        public IHttpActionResult AddWord(string word, int id)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var getList = _context.UserLists.SingleOrDefault(l => l.Id == id &&
                                                                  user.UserName == l.UserName);
            if (getList != null)
            {
                if (getList.Word.Contains(word))
                {
                    return BadRequest();
                }
                else
                {
                    getList.Word = getList.Word + word + ", ";

                }

            }

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteWord(string word, int id)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var getList = _context.UserLists.SingleOrDefault(l => l.Id == id &&
                                                                  user.UserName == l.UserName);
            if (getList != null)
            {
                var wordList = getList.Word.Split(',').Select(w => w.Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                if (wordList.Contains(word))
                {
                    wordList.Remove(word);
                    getList.Word = string.Join(", ", wordList.ToArray());
                    _context.SaveChanges();
                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}
