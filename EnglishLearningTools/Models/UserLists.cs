using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Web;
using TestGoogleApi.Models;

namespace EnglishLearningTools.Models
{
    public class UserLists
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int ListId { get; set; }
        public string ListName { get; set; }
        public string Word { get; set; }
        
        
    }
}