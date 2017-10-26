using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.ViewModels;

namespace Library.InterfacesLogic
{
    public interface IUserProfileLogic
    {
        UserProfileVM Login(UserProfileVM userProfile);
        void Register(UserProfileVM userProfile);
        void UpdateUserProfile(UserProfileVM userProfile);
        void Promote(int userProfileID);
        void Demote(int userProfileID);
        List<UserProfileVM> GetAllUserProfile();
        UserProfileVM GetUserProfileByID(int userProfileID);
    }
}
