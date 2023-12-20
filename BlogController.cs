using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Data;
using SimpleBlog.Models;
using SimpleBlog.Models.Domain;

namespace SimpleBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogDbContext mut;

        public BlogController(BlogDbContext gu)
        {
            mut = gu;
        }

        public IActionResult Index()
        {
            IEnumerable<BlogInfo> blogs = mut.Blogs.ToList();

            return View(blogs);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(BlogViewModel blog) {
            string t = blog.Title;
            string b = blog.Body;
            string iu = blog.ImgUrl;
            BlogInfo bloggo = new BlogInfo();
            bloggo.Title = t;
            bloggo.Body = b;    
            bloggo.ImgUrl = iu;
            mut.Add(bloggo);
            mut.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            BlogInfo blog = mut.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null) { return RedirectToAction("Index"); }
            return View(blog);
        }

        public IActionResult Edit(BlogInfo blog) {
            BlogInfo bloggo = mut.Blogs.Find(blog.Id);
            if (bloggo == null)
            {
                return RedirectToAction("Index");
            }

            bloggo.Title = blog.Title;
            bloggo.Body = blog.Body;
            bloggo.ImgUrl=blog.ImgUrl;
            mut.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            BlogInfo bloggo = mut.Blogs.Find(id);
            if(bloggo == null) { return RedirectToAction("Index"); }
            mut.Blogs.Remove(bloggo); mut.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
