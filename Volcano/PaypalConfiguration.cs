using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volcano
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;

        public readonly static string clientSecret;

        static PaypalConfiguration()
        {
            var config = getconfig();
            clientId = "AXb0skM9ROK_IvAmcEC0lrwAXv_x8jekh3oraIe2L_RllEXK1SMOhz7qpJvj2FhS9PRfblGWivKFe59R";
            clientSecret = "EPCqwmcP6K6mQyfS_lN2karQVVDwoUHY61IqgeUIlHL-8OtMOzN39ibKFncpvFkU22cziCY-k-zbAGD5";
        }

        private static Dictionary<string,string> getconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, getconfig()).GetAccessToken();

            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            APIContext aPIContext = new APIContext(GetAccessToken());
            aPIContext.Config = getconfig();

            return aPIContext;
        }
    }
}