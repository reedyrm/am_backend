using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AM_Backend.Models;

namespace AM_Backend
{
    [ServiceContract]
    public interface IAuthService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "user")]
        User CreateUser(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "GET",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "user/{username}")]
        User GetUser(string username);

        [OperationContract]
        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "login")]
        AuthObject VerifyUser(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "changepassword")]
        AuthObject ChangePassword(string username, string oldPassword, string newPassword, string confirmPassword);
    }

    [DataContract]
    public class AuthObject
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }
    }
}
