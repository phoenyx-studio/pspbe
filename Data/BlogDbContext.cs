using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using pspbe.Models;

namespace pspbe.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Category> Categories {get; set;}
        public virtual DbSet<Post> Posts {get; set;}
    }
    
}
