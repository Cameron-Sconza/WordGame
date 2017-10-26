using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.ViewModels;

namespace Library.InterfacesLogic
{
    public interface IWordBankLogic
    {
        bool NewWord(string word, int gameID);
    }
}
