using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.InterfacesRepo
{
    public interface IWordBankRepository
    {
        List<WordBankVM> GetAllWordBank();
        List<WordBankVM> GetGamesWordBank(int GameID);
        void AddWordBank(WordBankVM wordBank);
        void DeleteWordBank(int gameID);
    }
}
