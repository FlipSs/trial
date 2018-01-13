using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xm.Trial.Models;
using Xm.Trial.Models.Data;
using System;

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

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            Uri refUri;
            Uri.TryCreate(Request.Headers["Referer"], UriKind.RelativeOrAbsolute, out refUri);

            var postView = await _context.PostViews.FirstOrDefaultAsync(p => p.PostId == id && p.Views == refUri.Host) 
                ?? new PostView
            {
                PostId = id,
                Views = refUri.Host,
                Count = 0
            };
            postView.Count++;
            ValidateModel(postView);
            if (ModelState.IsValid) {
                if(await _context.PostViews.FirstOrDefaultAsync(p => p.PostId == id && p.Views == refUri.Host) == null)
                    _context.PostViews.Add(postView);
                await _context.SaveChangesAsync();
            }

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

        [HttpGet]
        public async Task<ActionResult> PostsStatistic()
        {
            var postsStatistics = await _context.Posts
                                    .Select(p => new PostStatisticViewModel
                                    {
                                        ID = p.Id,
                                        PostView = p.PostView,
                                        Likes = p.Likes
                                    })
                                    .OrderBy(p => p.ID)
                                    .ToArrayAsync();

            int totalPosts = postsStatistics.Length, totalViews = 0, totalLikes = 0;
            foreach(var post in postsStatistics)
            {
                totalLikes += post.Likes;
                foreach (var postView in post.PostView)
                {
                    post.TotalViews += postView.Count;
                }
                totalViews += post.TotalViews;
            }

            var postsStatisticsModel = new PostsStatisticViewModel
            {
                PostStatistic = postsStatistics,
                TotalPosts = totalPosts,
                TotalViews = totalViews,
                TotalLikes = totalLikes
            };

            return View(postsStatisticsModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();

            base.Dispose(disposing);
        }
    }
}