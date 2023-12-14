using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class MyPostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
