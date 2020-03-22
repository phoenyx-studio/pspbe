using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pspbe.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Created { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Updated { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}