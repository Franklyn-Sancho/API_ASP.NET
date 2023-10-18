using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PostsController : ControllerBase
    {

        private readonly MyApiContext _context;

        public PostsController(MyApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Posts>> GetPosts()
        {
            return _context.Posts.ToList();
        }


        [HttpPost]
        public ActionResult<Posts> CreatePosts(Posts posts)
        {


            _context.Posts.Add(posts);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPosts), new { id = posts.Id }, posts);
        }



    }
}