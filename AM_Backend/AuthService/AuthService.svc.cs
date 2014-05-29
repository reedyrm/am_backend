using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AM_Backend.DataService;
using AM_Backend.Models;

namespace AM_Backend
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuthRESTService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AuthRESTService.svc or AuthRESTService.svc.cs at the Solution Explorer and start debugging.
    public class AuthService : IAuthService
    {
        public User CreateUser(string username, string password)
        {
            //create user with username/password
            return UserDataService.CreateUser(username, password);
        }

        public User GetUser(string username)
        {
            //retrieve user 
            return UserDataService.GetUser(username);
        }

        public AuthObject VerifyUser(string username, string password)
        {
            AuthObject authObject = new AuthObject() {Success = false}; 

            if (username == "admin" && password == "password")
            {
                authObject.Username = username;
                authObject.Success = true;
                return authObject;
            }
            else
            {
                authObject.Success = false;
                return authObject;
            }

        }

        public AuthObject ChangePassword(string username, string oldPassword, string newPassword, string confirmPassword)
        {
            //update user password
            throw new NotImplementedException();
        }
    }
}
