using Library.Helpers;
using Library.InterfacesLogic;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shirirtoi.Controllers
{
    public class GameController : Controller
    {
        private IGameLogic Game;
        private IWordBankLogic WordBank;
        private IUserProfileLogic UserProfile;

        public GameController(IGameLogic gm, IWordBankLogic wb,
            IUserProfileLogic up)
        {
            Game = gm;
            WordBank = wb;
            UserProfile = up;
        }

        // GET: Game View All Games
        [AuthorizeHelper(Roles = "User, PowerUser, Administrator")]
        public ActionResult Index()
        {
            return View(Game.GetAllGames());
        }

        [HttpGet]
        [AuthorizeHelper(Roles = "PowerUser, Administrator")]
        public ActionResult CreateGame()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGame(GameVM game)
        {
            if (ModelState.IsValid)
            {
                Game.AddGame(game, (int)Session["ID"]);
                return RedirectToAction("Index", "Game", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [AuthorizeHelper(Roles = "User, PowerUser, Administrator")]
        public ActionResult PlayGame()
        {
            TempData["Play"] = false;
            PlayGameVM playGame = new PlayGameVM()
            {
                AllGames = Game.GetMyGames((int)Session["ID"])
            };
            return View(playGame);
        }

        [HttpPost]
        public ActionResult PlayGame(PlayGameVM playGame)
        {
            if (ModelState.IsValid)
            {
                if (WordBank.NewWord(playGame.Word, playGame.GameID))
                {
                    Game.AddWord(playGame, (int)Session["ID"]);
                    return RedirectToAction("PlayGame", "Game", new { area = "" });
                }
                else
                {
                    Game.WinLoss(playGame, (int)Session["ID"]);
                    return RedirectToAction("PlayGame", "Game", new { area = "" });
                }
            }
            else
            {
                return View("PlayGame");
            }
        }
        
        public ActionResult JoinGame(int gameID)
        {
            Game.JoinGame(gameID, (int)Session["ID"]);
            return RedirectToAction("Index", "Game", new { area = "" });
        }
        
        public ActionResult LeaveGame(int gameID)
        {
            Game.LeaveGame(gameID, (int)Session["ID"]);
            return RedirectToAction("PlayGame", "Game", new { area = "" });
        }
        
        public ActionResult DeleteGame(int gameID)
        {
            Game.DeleteGame(gameID);
            return RedirectToAction("Index", "Game", new { area = "" });
        }
        
        public ActionResult ChosenGame(int gameID)
        {
            TempData["Play"] = true;
            PlayGameVM playGame = Game.SelectGame(gameID);
            playGame.AllGames = Game.GetMyGames((int)Session["ID"]);
            return View("PlayGame", playGame);
        }
    }
}