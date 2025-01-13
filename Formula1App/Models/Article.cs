using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.Models
{
    public class Article
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsBreaking { get; set; }
        public List<Subject> Subjects { get; set; }
        public Subject FirstSubject { get; set; }
        public Article() { }
    }
}
