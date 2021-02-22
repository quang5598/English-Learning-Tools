using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using EnglishLearningTools.Models;
using EnglishLearningTools.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TestGoogleApi.Models;

namespace EnglishLearningTools.Controllers
{
    public class DictionaryController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> UserManager { get; set; }

        public DictionaryController()
        {
            _context = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }
        // GET: Dictionary

        public ActionResult Index(string word = "")
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var listsViewModel = new ListsViewModel();
            if (user != null)
            {
                var getCurrentLists = _context.UserLists.Where(l => l.UserName == user.UserName).ToList();

                var getLists = new Dictionary<int, string>();
                foreach (var list in getCurrentLists)
                {
                    
                    getLists.Add(list.Id, list.ListName);
                }

                listsViewModel.Word = word;
                listsViewModel.ListDictionary = getLists;
                
                return View(listsViewModel);
            }

            listsViewModel.Word = word;
            return View(listsViewModel);



        }
        
        



        public ActionResult New()
        {
           
            return View();
        }
    }
}