using AutoMapper;
using Library.EFModels;
using Library.ViewModels;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository() : base(new RepositoryContext("Shiritori")) { }

        public void AddRole(RoleVM role)
        {
            Add(Mapper.Map<Role>(role));
            Save();
        }
    }
}
