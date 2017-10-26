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
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository() : base(new RepositoryContext("Shiritori")) { }

        public List<UserProfileVM> GetAllUserProfiles()
        {
            var userProfile = DbSet.ToList();
            return Mapper.Map<List<UserProfileVM>>(userProfile).ToList();
        }

        public UserProfileVM  GetUserProfileByID(int userProfileID)
        {
            var userProfile = GetAllUserProfiles().Where(u => u.UserProfileID == userProfileID).FirstOrDefault();
            return Mapper.Map<UserProfileVM>(userProfile);
        }

        public void AddUserProfile(UserProfileVM userProfile)
        {
            Add(Mapper.Map<UserProfile>(userProfile));
            Save();
        }

        public void UpdateUserProfile(UserProfileVM userProfile)
        {
            Update(Mapper.Map<UserProfile>(userProfile));
            Save();
        }

        public UserProfileVM GetUserProfileByID(int? userProfileID)
        {
            var userProfile = GetAllUserProfiles().Where(u => u.UserProfileID == userProfileID).FirstOrDefault();
            return Mapper.Map<UserProfileVM>(userProfile);
        }

        public UserProfileVM GetUserProfileByUserName(string username)
        {
            var userProfile = GetAllUserProfiles().Where(u => u.Username == username).FirstOrDefault();
            return Mapper.Map<UserProfileVM>(userProfile);
        }
    }
}
