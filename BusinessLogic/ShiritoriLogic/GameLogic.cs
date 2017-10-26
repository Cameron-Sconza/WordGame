using Library.InterfacesLogic;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.ViewModels;
using Library.InterfacesRepo;

namespace BusinessLogic.ShiritoriLogic
{
    public class GameLogic : IGameLogic
    {
        private IGameRepository Game;
        private IWordBankRepository WordBank;
        private IUserProfileRepository UserProfile;

        public GameLogic(IGameRepository gm,
            IWordBankRepository wb,
            IUserProfileRepository up)
        {
            Game = gm;
            WordBank = wb;
            UserProfile = up;
        }

        public List<GameVM> GetAllGames()
        {
            var gameList = Game.GetAllGames();
            foreach(var value in gameList)
            {
                value.Players = FillPlayers(value.PlayersID);
            }
            return gameList;
        }

        public List<GameVM> GetMyGames(int userProfileID)
        {
            var gameList = Game.GetMyGames(userProfileID);
            foreach (var value in gameList)
            {
                value.Players = FillPlayers(value.PlayersID);
            }
            return gameList;
        }

        public PlayGameVM SelectGame(int gameID)
        {
            var playGame = Mapper.Map<PlayGameVM>(Game.GetGameByID(gameID));
            playGame.Players = FillPlayers(playGame.PlayersID);
            playGame.PlayerTurnName = UserProfile.GetUserProfileByID(playGame.PlayerTurn);
            playGame.WordBank = WordBank.GetGamesWordBank(playGame.GameID);
            return playGame;
        }

        private UserProfileVM[] FillPlayers(int?[] IDs)
        {
            UserProfileVM[] Players = new UserProfileVM[5];
            Players[0] = UserProfile.GetUserProfileByID(IDs[0]);
            Players[1] = UserProfile.GetUserProfileByID(IDs[1]);
            Players[2] = UserProfile.GetUserProfileByID(IDs[2]);
            Players[3] = UserProfile.GetUserProfileByID(IDs[3]);
            Players[4] = UserProfile.GetUserProfileByID(IDs[4]);
            return Players;
        }

        public void AddGame(GameVM game, int userProfileID)
        {
            game.Active = true;
            game.PlayerTurn = userProfileID;
            game.PlayerOne = userProfileID;
            game.LastLetter = GetLetter();
            Game.AddGame(game);
        }

        private string GetLetter()
        {
            Random rnd = new Random();
            int num = rnd.Next(0, 26);
            char let = (char)('a' + num);
            return let.ToString();
        }

        public void DeleteGame(int gameID)
        {
            WordBank.DeleteWordBank(gameID);
            Game.DeleteGame(gameID);
        }

        public void JoinGame(int gameID, int userProfileID)
        {
            GameVM join = Game.GetGameByID(gameID);
            if (join.PlayerTurn == null)
            {
                join.PlayerTurn = userProfileID;
            }
            if (join.PlayerOne == null)
            {
                join.PlayerOne = userProfileID;
                Game.UpdateGame(join);
            }
            else if (join.PlayerTwo == null)
            {
                join.PlayerTwo = userProfileID;
                Game.UpdateGame(join);
            }
            else if (join.PlayerThree == null)
            {
                join.PlayerThree = userProfileID;
                Game.UpdateGame(join);
            }
            else if (join.PlayerFour == null)
            {
                join.PlayerFour = userProfileID;
                Game.UpdateGame(join);
            }
            else
            {
                join.PlayerFive = userProfileID;
                Game.UpdateGame(join);
            }
        }

        public void LeaveGame(int gameID, int userProfileID)
        {
            GameVM leave = Game.GetGameByID(gameID);
            if(leave.PlayerTurn == userProfileID)
            {
                if (leave.PlayersID.Count(i => i != null) > 1)
                {
                    leave.PlayerTurn = NextTurnLeave(leave, userProfileID);
                }
                else
                {
                    leave.PlayerTurn = null;
                }
            }
            if (leave.PlayerOne == userProfileID)
            {
                leave.PlayerOne = null;
                Game.UpdateGame(leave);
            }
            else if (leave.PlayerTwo == userProfileID)
            {
                leave.PlayerTwo = null;
                Game.UpdateGame(leave);
            }
            else if (leave.PlayerThree == userProfileID)
            {
                leave.PlayerThree = null;
                Game.UpdateGame(leave);
            }
            else if (leave.PlayerFour == userProfileID)
            {
                leave.PlayerFour = null;
                Game.UpdateGame(leave);
            }
            else
            {
                leave.PlayerFive = null;
                Game.UpdateGame(leave);
            }
        }

        public void UpdateGame(GameVM game)
        {
            Game.UpdateGame(game);
        }

        public void WinLoss(PlayGameVM playGame, int userProfileID)
        {
            foreach (var player in playGame.Players)
            {
                if (player != null)
                {
                    if (userProfileID == player.UserProfileID)
                    {
                        player.Wins += 1;
                        player.OverAllScore += playGame.Score;
                        UserProfile.UpdateUserProfile(player);
                    }
                    else
                    {
                        player.Losses += 1;
                        UserProfile.UpdateUserProfile(player);
                    }
                }
            }
            GameVM game = Game.GetGameByID(playGame.GameID);
            game.Score = 0;
            game.LastLetter = GetLetter();
            game.PlayerTurn = NextTurnLeave(game, userProfileID);
            Game.UpdateGame(game);
            WordBank.DeleteWordBank(game.GameID);
        }

        public void AddWord(PlayGameVM playGame, int userProfileID)
        {
            WordBankVM wordBank = new WordBankVM()
            {
                Words = playGame.Word,
                GameID = playGame.GameID
            };
            AddScore(playGame, userProfileID);
            WordBank.AddWordBank(wordBank);
        }

        private void AddScore(PlayGameVM playGame, int userProfileID)
        {
            var game = Game.GetGameByID(playGame.GameID);
            game.Score += (playGame.Word.Length * 5);
            game.LastLetter = playGame.Word[(playGame.Word.Length - 1)].ToString();
            NextTurn(game, userProfileID);
        }

        private int? NextTurnLeave(GameVM game, int userProfileID)
        {
            int playerCount = game.PlayersID.Count(i => i != null);
            if (playerCount == 1)
            {
                game.PlayerTurn = userProfileID;
            }
            else
            {
                for (int i = 0; i < playerCount; i++)
                {
                    if (i == playerCount)
                    {
                        if (game.PlayersID[i] == userProfileID)
                        {
                            game.PlayerTurn = game.PlayersID[0];
                            break;
                        }
                    }
                    else
                    {
                        if (game.PlayersID[i] == userProfileID)
                        {
                            if ((i + 1) == playerCount || (i + 1) > playerCount)
                            {
                                game.PlayerTurn = game.PlayersID[0];
                                break;
                            }
                            else
                            {
                                if (game.PlayersID[i] == userProfileID)
                                {
                                    game.PlayerTurn = game.PlayersID[i + 1];
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return game.PlayerTurn;
        }

        private void NextTurn(GameVM game, int userProfileID)
        {
            int playerCount = game.PlayersID.Count(i => i != null);
            if (playerCount == 1)
            {
                game.PlayerTurn = userProfileID;
                Game.UpdateGame(game);
            }
            else
            {
                for (int i = 0; i < playerCount; i++)
                {
                    if (i == playerCount)
                    {
                        if (game.PlayersID[i] == userProfileID)
                        {
                            game.PlayerTurn = game.PlayersID[0];
                            Game.UpdateGame(game);
                            break;
                        }
                    }
                    else
                    {
                        if (game.PlayersID[i] == userProfileID)
                        {
                            if ((i + 1) == playerCount || (i + 1) > playerCount)
                            {
                                game.PlayerTurn = game.PlayersID.Where(p => p != null).FirstOrDefault();
                                Game.UpdateGame(game);
                                break;
                            }
                            else
                            {
                                if (game.PlayersID[i] == userProfileID)
                                {
                                    game.PlayerTurn = game.PlayersID[i + 1];
                                    Game.UpdateGame(game);
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }
}
