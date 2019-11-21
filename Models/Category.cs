using System;
using System.Collections.Generic;

namespace pspbe.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}