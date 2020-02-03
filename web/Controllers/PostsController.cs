using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Entities;

namespace web.Controllers
{
    public class PostsController : Controller
    {
        readonly MyDbContext _dbContext;

        public PostsController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _dbContext.Posts.ToListAsync();

            return View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {

            var postToAdd = new Post
            {
                Html = post.Html,
                Created = DateTimeOffset.Now
            };

            _dbContext.Posts.Add(postToAdd);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}