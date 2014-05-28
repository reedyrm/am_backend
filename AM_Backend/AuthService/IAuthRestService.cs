using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AM_Backend
{
    [ServiceContract]
    public interface IAuthRestService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", 
                   RequestFormat = WebMessageFormat.Json, 
                   ResponseFormat = WebMessageFormat.Json, 
                   UriTemplate = "login")]
        AuthObject VerifyUser(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "POST", 
                   RequestFormat = WebMessageFormat.Json, 
                   ResponseFormat = WebMessageFormat.Json, 
                   UriTemplate = "changepassword")]
        AuthObject ChangePassword(string username, string oldPassword, string newPassword, string confirmPassword);
    }
}
