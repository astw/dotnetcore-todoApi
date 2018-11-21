using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcoreToDoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TodoApi.Models;

namespace dotnetcoreToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {

        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService, IConfiguration configuration)
        {
            _blogService = blogService;

            var t = configuration.GetSection("host1").Value; 
        }

        [HttpGet]
        public ActionResult<List<Blog>> GetAll()
        {
            return _blogService.GetAllBlogs().ToList();
        }
 
        [HttpGet("users/{userId}", Name = "GetUserBlogs")]
        public ActionResult<List<Blog>> GetBlogsByUser(int userId)
        {
            return _blogService.GetAuthorBlogs(userId).ToList();
        }
    }
}