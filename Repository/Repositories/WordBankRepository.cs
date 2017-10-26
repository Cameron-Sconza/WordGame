using Library.EFModels;
using Library.InterfacesRepo;
using Repository.Context;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.ViewModels;

namespace Repository.Repositories
{
    public class WordBankRepository : BaseRepository<WordBank>, IWordBankRepository
    {
        public WordBankRepository() : base(new RepositoryContext("Shiritori")) { }

        public List<WordBankVM> GetAllWordBank()
        {
            var wordBank = DbSet.ToList();
            return Mapper.Map<List<WordBankVM>>(wordBank);
        }

        public List<WordBankVM> GetGamesWordBank(int GameID)
        {
            var wordBank = GetAllWordBank().Where(w => w.GameID == GameID).ToList();
            return Mapper.Map<List<WordBankVM>>(wordBank).ToList();
        }

        public void AddWordBank(WordBankVM wordBank)
        {
            Add(Mapper.Map<WordBank>(wordBank));
            Save();
        }
        public void DeleteWordBank(int gameID)
        {
            foreach (var wordBank in GetGamesWordBank(gameID))
            {
                Delete(wordBank.WordBankID);
                Save();
            }
        }
    }
}
