using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EFModels
{
   public  class UserProfile
    {
        public int UserProfileID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int TotalGames { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int OverAllScore { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
    }
}
