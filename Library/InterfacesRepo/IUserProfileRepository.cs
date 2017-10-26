using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.InterfacesRepo
{
    public interface IUserProfileRepository
    {
        List<UserProfileVM> GetAllUserProfiles();
        UserProfileVM GetUserProfileByID(int userProfileID);
        void UpdateUserProfile(UserProfileVM userProfile);
        UserProfileVM GetUserProfileByID(int? userProfileID);
        void AddUserProfile(UserProfileVM userProfile);
        UserProfileVM GetUserProfileByUserName(string username);
    }
}
