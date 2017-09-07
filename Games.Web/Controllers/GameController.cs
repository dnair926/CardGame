using Microsoft.AspNetCore.Mvc;

namespace CardGame.Web.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Body()
        {
            return PartialView();
        }
    }
}