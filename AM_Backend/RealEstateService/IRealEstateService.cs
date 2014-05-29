using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AM_Backend.RealEstateService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRealEstateService" in both code and config file together.
    [ServiceContract]
    public interface IRealEstateService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "listings/{zipcode}")]
        List<RealEstateListing> GetListings(string zipcode);
    }

    [DataContract]
    public class RealEstateListing
    {
        [DataMember]
        public string ZillowId { get; set; }

        [DataMember]
        public string HouseNumber { get; set; }

        [DataMember]
        public string StreetName { get; set; }

        [DataMember]
        public string ZipCode { get; set; }
    }
}
