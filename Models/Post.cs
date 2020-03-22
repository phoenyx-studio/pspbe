using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pspbe.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Created { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Updated { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}