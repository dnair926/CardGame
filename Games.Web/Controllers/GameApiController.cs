using CardGame.ClassLibrary;
using Microsoft.AspNetCore.Mvc;

namespace CardGame.Web.Controllers
{
    [Route("api/[controller]")]
    public class GameApiController : Controller
    {
        [HttpGet("[action]")]
        public Game GetGame(int playerCount)
        {
            var gameService = new GameService();
            var game = gameService.CreateGame(playerCount);

            return game;
        }

        [HttpPost("[action]")]
        public void PlayCard(Player player, Card card)
        {
            var gameService = new GameService();
            gameService.PlayCard(player, card);
        }

    }
}
