using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.InterfacesRepo
{
    public interface IGameRepository
    {
        List<GameVM> GetAllGames();
        GameVM GetGameByID(int gameID);
        void AddGame(GameVM game);
        void UpdateGame(GameVM game);
        void DeleteGame(int gameID);
        List<GameVM> GetMyGames(int userProfileID);
    }
}
