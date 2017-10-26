using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class UserProfileVM
    {
         public int UserProfileID { get; set; }
        [Required(ErrorMessage = "This Field Is Required.")]
        [MaxLength(50, ErrorMessage = "Cannot Be More Then 50 Characters Long.")]
        [RegularExpression(@"^[-a-zA-Z0-9\S]+", ErrorMessage = "Not All Special Characters/Symbols Are Allowed.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This Field Is Required.")]
        [MaxLength(30, ErrorMessage = "Cannot Be More Then 30 Characters Long.")]
        [RegularExpression(@"^[-a-zA-Z0-9\S]+", ErrorMessage = "Not All Special Characters/Symbols Are Allowed.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "This Field Is Required.")]
        [RegularExpression(@"^[-a-zA-Z0-9\S]+", ErrorMessage = "Not All Special Characters/Symbols Are Allowed.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int TotalGames { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int OverAllScore { get; set; }
        public int RoleID { get; set; }
        public RoleVM Role { get; set; }
    }
}
