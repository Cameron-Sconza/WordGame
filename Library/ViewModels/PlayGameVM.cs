using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class PlayGameVM
    {
        public int GameID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "The Name Can Be No More The 50 Characters")]
        public string GameName { get; set; }
        public int? PlayerOne { get; set; }
        public int? PlayerTwo { get; set; }
        public int? PlayerThree { get; set; }
        public int? PlayerFour { get; set; }
        public int? PlayerFive { get; set; }
        public UserProfileVM[] Players { get; set; }
        public int?[] PlayersID { get; set; }
        public bool Active { get; set; }
        public int? PlayerTurn { get; set; }
        public UserProfileVM PlayerTurnName { get; set; }
        public int Score { get; set; }
        public string LastLetter { get; set; }
        public List<WordBankVM> WordBank { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Cannot Be More Then 50 Characters Long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Letters Only.")]
        public string Word { get; set; }
        public List<GameVM> AllGames { get; set; }

        public PlayGameVM()
        {
            AllGames = new List<GameVM>();
            WordBank = new List<WordBankVM>();
            Players = new UserProfileVM[5];
        }
    }
}
