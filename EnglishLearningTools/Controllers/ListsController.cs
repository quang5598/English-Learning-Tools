using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using EnglishLearningTools.Models;
using EnglishLearningTools.Models.ViewModel;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EnglishLearningTools.Controllers
{
    [Authorize]
    public class ListsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> UserManager { get; set; }

        public ListsController()
        {
            _context = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

        }
        // GET: Lists
        public ActionResult Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            
            var list = _context.UserLists.Where(l => l.UserName == user.UserName).ToList();
            return View(list);
        }

        public ActionResult NewList()
        {
            return View();
        }

        public ActionResult AddList(UserLists list)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var result = _context.UserLists.Where(l => l.ListName.ToUpper().Contains(list.ListName.ToUpper())
            && l.UserName == user.UserName);
            if (!result.Any())
            {
                var newList = new UserLists()
                {
                    ListName = list.ListName,
                    UserName = user.UserName
                };
                if (!list.Word.IsNullOrWhiteSpace())
                {
                    //char[] delimiters = new[] { ',', ';', ' ', '\n', '"','?', '^', '*',  };
                    //var words = list.Word.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
                    var words = Regex.Replace(list.Word, "[^0-9a-zA-Z']+", ",");
                    var wordArray = words.Split(',').Distinct().ToArray();
                    newList.Word = string.Join(", ", wordArray);
                }
                else
                {
                    newList.Word = "";
                }
                _context.UserLists.Add(newList);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("ListName", list.ListName);
                return View("NewList");
            }
            

        }

        public ActionResult MyList(int? id)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var getWord = _context.UserLists.SingleOrDefault(l => l.UserName == user.UserName 
                                                                  && l.Id == id);
            if (getWord != null && getWord.Word != null)
            {
                var wordList = getWord.Word.Split(',').Select(w => w.Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                var listViewModel = new ListsViewModel()
                {
                    ListName = getWord.ListName,
                    Words = wordList,
                    ListId = getWord.Id
                };
                return View(listViewModel);
            }
            else if (getWord != null && getWord.ListName != null)
            {
                var listViewModel = new ListsViewModel()
                {
                    ListName = getWord.ListName,
                    
                    
                };
                return View(listViewModel);
            }

            return View();


        }
    }
}