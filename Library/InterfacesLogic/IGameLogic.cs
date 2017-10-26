using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.ViewModels;

namespace Library.InterfacesLogic
{
    public interface IGameLogic
    {
        List<GameVM> GetAllGames();
        List<GameVM> GetMyGames(int userProfileID);
        void AddGame(GameVM game, int userProfileID);
        void DeleteGame(int gameID);
        void LeaveGame(int gameID, int userProfileID);
        void JoinGame(int gameID, int userProfileID);
        PlayGameVM SelectGame(int gameID);
        void UpdateGame(GameVM game);
        void WinLoss(PlayGameVM playGame, int userProfileID);
        void AddWord(PlayGameVM playGame, int userProfileID);
    }
}
