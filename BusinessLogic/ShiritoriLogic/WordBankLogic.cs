using Library.InterfacesLogic;
using Library.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ShiritoriLogic
{
    public class WordBankLogic : IWordBankLogic
    {
        private IWordBankRepository WordBank;
        private IDictionaryRepository Dictionary;

        public WordBankLogic(IWordBankRepository wb, IDictionaryRepository dictionary)
        {
            WordBank = wb;
            Dictionary = dictionary;
        }
        public bool NewWord(string word, int gameID)
        {
            if (Dictionary.WordCheck(word))
            {
                if (WordBank.GetAllWordBank().Where(w => w.Words == word && w.GameID == gameID).FirstOrDefault() == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
