using Library.EFModels;
using Library.InterfacesRepo;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DictionaryRepository : BaseRepository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository() : base(new RepositoryContext("Shiritori")) { }

        public void AddDictionary(List<Dictionary> dictionary)
        {
            AddBulk(dictionary);
            Save();
        }

        public bool WordCheck(string word)
        {
            Dictionary exists = new Dictionary();
            if ((exists = DbSet.Where(d=> d.Word == word).FirstOrDefault()) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
