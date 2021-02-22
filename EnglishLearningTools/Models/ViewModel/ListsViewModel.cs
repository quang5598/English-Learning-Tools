using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishLearningTools.Models.ViewModel
{
    public class ListsViewModel
    {
        public UserLists Lists { get; set; }
        public IEnumerable<string> Words { get; set; }
        public string ListName { get; set; }    
        public string Word { get; set; }
        public IEnumerable<string> ListNames { get; set; }
        public IEnumerable<int> Ids { get; set; }
        public IDictionary<int,string> ListDictionary { get; set; }
        public int ListId { get; set; }     
      
    }
}