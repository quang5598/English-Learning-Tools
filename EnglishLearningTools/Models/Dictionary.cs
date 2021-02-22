using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestGoogleApi.Models
{
    public class Dictionary
    {
        public string Str { get; set; }
        public string Query { get; set; }
        public List<string> Definition { get; set; }
        public string PartOfSpeech { get; set; }
        public int Frequency { get; set; }
        public string Word { get; set; }
    }
}