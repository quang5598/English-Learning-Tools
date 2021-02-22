using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EnglishLearningTools.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EnglishLearningTools.Controllers.API
{
    
    public class ListController : ApiController
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> UserManager { get; set; }

        public ListController()
        {
            _context = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }

        [HttpDelete]
        public IHttpActionResult DeleteList(int id)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var getList = _context.UserLists.SingleOrDefault(l => l.Id == id &&
                                                                  user.UserName == l.UserName);
            if (getList != null)
            {
                _context.UserLists.Remove(getList);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
           

        }
    }
}
