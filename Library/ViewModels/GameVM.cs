using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class GameVM
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
        public int Score { get; set; }
        public string LastLetter { get; set; }
        public List<WordBankVM> WordBank { get; set; }
        public GameVM()
        {
            WordBank = new List<WordBankVM>();
            Players = new UserProfileVM[5];
            PlayersID = new int?[5];
        }

    }
}
