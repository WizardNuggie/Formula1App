using Formula1App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsBreaking { get; set; }
        public int WriterId { get; set; }
        public int StatusId { get; set; }
        public List<Subject> Subjects { get; set; }
        public Subject FirstSubject { get; set; }
        public string FullImagePath
        {
            get
            {
                return F1IntService.ImageBaseAddress + $"articles/{Id}.png";
            }
        }
        public Article() { }
    }
}
