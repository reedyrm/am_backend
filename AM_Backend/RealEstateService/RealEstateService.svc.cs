using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace AM_Backend.RealEstateService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RealEstateService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RealEstateService.svc or RealEstateService.svc.cs at the Solution Explorer and start debugging.
    public class RealEstateService : IRealEstateService
    {
        public List<RealEstateListing> GetListings(string zipcode)
        {
            //call out to zillow.com

            Regex regex = new Regex("\"/homedetails/(.*?)/(?<zid>\\d+).*?\"");

            List<RealEstateListing> list = new List<RealEstateListing>();

            int page = 1;
            while (true)
            {
                string url = String.Format("http://www.zillow.com/homes/for_sale/{0}/list", zipcode);
                if (page > 1)
                {
                    url = String.Format("http://www.zillow.com/homes/for_sale/{0}/list/{1}_p/", zipcode, page);
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();

                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                string resultPage = readStream.ReadToEnd();

                //process
               
                MatchCollection matches = regex.Matches(resultPage);
                Dictionary<String, Match> dupeCheck = new Dictionary<string, Match>();
                foreach (Match m in matches)
                {
                    //double checks for dupes
                    if (!dupeCheck.ContainsKey(m.Value))
                    {
                        RealEstateListing listing = new RealEstateListing();
                        listing.ZillowId = m.Groups["zid"].Value;
                        listing.ZipCode = zipcode;
                        listing.StreetName = m.Groups[1].Value;
                        list.Add(listing);

                        dupeCheck.Add(m.Value, m);
                    }
                }

                if (!resultPage.Contains("Next page"))
                    break;

                page++;
            }

            return list;
        }
    }
}
