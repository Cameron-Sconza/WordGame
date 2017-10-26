using Library.InterfacesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Helpers;
using Library.ViewModels;
using Library.InterfacesRepo;

namespace BusinessLogic.ShiritoriLogic
{
    public class UserProfileLogic : IUserProfileLogic
    {
        private IUserProfileRepository UserProfile;

        public UserProfileLogic(IUserProfileRepository up)
        {
            UserProfile = up;
        }
        public void Demote(int userProfileID)
        {
            UserProfileVM user = UserProfile.GetUserProfileByID(userProfileID);
            user.RoleID += 1;
            UserProfile.UpdateUserProfile(user);
        }

        public List<UserProfileVM> GetAllUserProfile()
        {
            return UserProfile.GetAllUserProfiles();
        }

        public UserProfileVM GetUserProfileByID(int userProfileID)
        {
            return UserProfile.GetUserProfileByID(userProfileID);
        }

        public UserProfileVM Login(UserProfileVM userProfile)
        {
            HashHelper hashHelper = new HashHelper();
            UserProfileVM user = UserProfile.GetUserProfileByUserName(userProfile.Username);
            if (hashHelper.GetHash(userProfile.Password) == user.Password)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public void Promote(int userProfileID)
        {
            UserProfileVM user = UserProfile.GetUserProfileByID(userProfileID);
            user.RoleID -= 1;
            UserProfile.UpdateUserProfile(user);
        }

        public void Register(UserProfileVM userProfile)
        {
            HashHelper hashHelper = new HashHelper();
            userProfile.Password = hashHelper.GetHash(userProfile.Password);
            userProfile.RoleID = 3;
            UserProfile.AddUserProfile(userProfile);
        }

        public void UpdateUserProfile(UserProfileVM userProfile)
        {
            HashHelper hashHelper = new HashHelper();
            userProfile.Password = hashHelper.GetHash(userProfile.Password);
            UserProfile.UpdateUserProfile(userProfile);
        }
    }
}
