using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using TodoApi.Models;

namespace dotnetcoreToDoApi.Services
{
    public interface IBlogService
    {
        ICollection<Blog> GetAllBlogs();
        ICollection<Blog> GetAuthorBlogs(int userId);
    }


    public class BlogServices : IBlogService
    {
        private readonly BloggingContext _bloggingContext;
        private readonly ILogger _logger;

        public BlogServices(BloggingContext context, ILogger<BlogServices> logger)
        {
            _bloggingContext = context;
            _logger = logger; 
        }

        public ICollection<Blog> GetAllBlogs()
        {
            _logger.LogDebug("Get all blogs");
            return _bloggingContext.Blogs.ToList();   
        }

        public ICollection<Blog> GetAuthorBlogs(int userId)
        {
            return _bloggingContext.Blogs.Where(i => i.UserId == userId).ToList();
        }
    }
}
