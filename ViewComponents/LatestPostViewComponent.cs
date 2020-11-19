// Link to tutorial https://docs.microsoft.com/ru-ru/aspnet/core/mvc/views/view-components?view=aspnetcore-5.0
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using pspbe.Data;
using pspbe.Models;

namespace pspbe.ViewComponents
{
    public class LatestPostViewComponent : ViewComponent
    {
        private readonly BlogDbContext _context;

        public LatestPostViewComponent(BlogDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var post = _context.Posts.OrderBy(post => post.Created).First();
            return View(post);
        }

    }
}