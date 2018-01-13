using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xm.Trial.Models;
using Xm.Trial.Models.Data;

namespace Xm.Trial.Controllers
{
    public class BlogController : Controller
    {
        private readonly DataContext _context;

        public BlogController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int? curId)
        {
            int id = (curId == null || !Request.IsAjaxRequest()) ? 0 : curId.Value;

            var posts = await _context.Posts
                                 .OrderBy(p => p.Id)
                                 .Select(p => new PostSnippetViewModel
                                              {
                                                  Id = p.Id,
                                                  Created = p.Created,
                                                  Title = p.Title,
                                                  Picture = p.Picture,
                                                  PictureCaption = p.PictureCaption,
                                                  Snippet = p.Snippet,
                                                  Author = p.Author
                                              })
                                 .Where(p => p.Id > id)
                                 .Take(5)
                                 .ToArrayAsync();

            var viewModel = new BlogViewModel
                            {
                                Title = "Posts",
                                Posts = posts
                            };
            if (id != 0)
            {
                return PartialView("_BlogPart", viewModel);
            }

            return View(viewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            var post = await _context.Posts
                                .Select(p => new PostViewModel
                                             {
                                                 Id = p.Id,
                                                 Created = p.Created,
                                                 Title = p.Title,
                                                 Picture = p.Picture,
                                                 PictureCaption = p.PictureCaption,
                                                 Text = p.Text,
                                                 Likes = p.Likes,
                                                 Author = p.Author,
                                                 Tags = p.Tags
                                             })
                                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return new HttpNotFoundResult();

            ViewBag.Title = post.Title;

            return View(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();

            base.Dispose(disposing);
        }
    }
}