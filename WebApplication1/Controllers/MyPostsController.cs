using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class MyPostsController : Controller
    {
        private readonly ILogger<MyPostsController> _logger;

        public MyPostsController(ILogger<MyPostsController> logger)
        {
            _logger = logger;
        }

        public IActionResult MyPostsList()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
