using System;
using System.Collections.Generic;

namespace pspbe.Models 
{
    public class Category
    {
        public int Id {get; set;}
        public string Title {get; set;}
        public ICollection<Post> Posts {get; set;}
    }
}