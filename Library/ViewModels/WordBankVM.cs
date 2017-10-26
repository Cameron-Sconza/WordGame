using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class WordBankVM
    {
        public int WordBankID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Cannot Be More Then 50 Characters Long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Letters Only.")]
        public string Words { get; set; }
        public int GameID { get; set; }
    }
}
