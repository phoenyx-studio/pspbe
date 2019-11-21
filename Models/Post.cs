using System;
using System.Collections.Generic;

namespace pspbe.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}