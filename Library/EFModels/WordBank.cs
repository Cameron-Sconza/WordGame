using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EFModels
{
    public class WordBank
    {
        public int WordBankID { get; set; }
        public string Words { get; set; }
        public int GameID { get; set; }
    }
}
