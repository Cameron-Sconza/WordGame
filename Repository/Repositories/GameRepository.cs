using AutoMapper;
using Library.EFModels;
using Library.InterfacesRepo;
using Library.ViewModels;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository() : base(new RepositoryContext("Shiritori")) { }

        public List<GameVM> GetAllGames()
        {
            var games = DbSet.Include("UserProfile").ToList();
            var GameVMList = Mapper.Map<List<GameVM>>(games);
            List<GameVM> list = new List<GameVM>();
            foreach (var item in GameVMList)
            {
                list.Add(FillPlayers(item));
            }
            return list;
        }

        public GameVM GetGameByID(int gameID)
        {
            var game = GetAllGames().Where(g => g.GameID == gameID).FirstOrDefault();
            game = FillPlayers(game);
            return Mapper.Map<GameVM>(game);
        }

        public List<GameVM> GetMyGames(int userProfileID)
        {
            var game = GetAllGames().Where(g => g.PlayerOne == userProfileID ||
                g.PlayerTwo == userProfileID ||
                g.PlayerThree == userProfileID ||
                g.PlayerFour == userProfileID ||
                g.PlayerFive == userProfileID).ToList();
            return Mapper.Map<List<GameVM>>(game);
        }

        public void AddGame(GameVM game)
        {
            Add(Mapper.Map<Game>(game));
            Save();
        }

        public void UpdateGame(GameVM game)
        {
            Update(Mapper.Map<Game>(game));
            Save();
        }

        public void DeleteGame(int gameID)
        {
            Delete(gameID);
            Save();
        }

        private GameVM FillPlayers(GameVM game)
        {
            game.PlayersID[0] = game.PlayerOne;
            game.PlayersID[1] = game.PlayerTwo;
            game.PlayersID[2] = game.PlayerThree;
            game.PlayersID[3] = game.PlayerFour;
            game.PlayersID[4] = game.PlayerFive;
            return game;
        }
    }
}
