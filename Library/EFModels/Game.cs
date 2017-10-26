using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EFModels
{
    public class Game
    {
        public int GameID { get; set; }
        public string GameName { get; set; }
        public int? PlayerOne { get; set; }
        public int? PlayerTwo { get; set; }
        public int? PlayerThree { get; set; }
        public int? PlayerFour { get; set; }
        public int? PlayerFive { get; set; }
        public bool Active { get; set; }
        public int? PlayerTurn { get; set; }
        public int Score { get; set; }
        public string LastLetter { get; set; }
        public virtual ICollection<UserProfile> UserProfile { get; set; }
        public ICollection<WordBank> WordBank { get; set; }
    }
}
