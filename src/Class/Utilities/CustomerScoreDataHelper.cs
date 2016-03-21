using Microsoft.Phone.Net.NetworkInformation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Tap.Class.Utilities
{
    public class CustomerScoreDataHelper
    {
        private const string PHP_SERVER_PAGE_URL = "http://visualdev.esy.es/files/Projets/FastTap/ranking.php";
        private const string READ_PROCESS = "readdata";
        private const string ADD_PROCESS = "adddata";
        private const string CODE_PROPERTY = "code";
        private const string REQUEST_CODE_ERROR = "1";
        private const byte RANKING_LIMIT = 10;
        private byte rankingLimit;

        public CustomerScoreDataHelper(byte rankingLimit = RANKING_LIMIT)
        {
            this.rankingLimit = rankingLimit;
        }

        private bool IsConnected()
        {
            var type = NetworkInterface.NetworkInterfaceType;

            bool result = false;
            if ((type == NetworkInterfaceType.Wireless80211) ||
                (type == NetworkInterfaceType.MobileBroadbandCdma) || 
                (type == NetworkInterfaceType.MobileBroadbandGsm))
                result = true;
            else if (type == NetworkInterfaceType.None)
                result = false;
            return result;
        }

        public async Task<List<CustomerScore>> GetRankingAsync()
        {
            if(!this.IsConnected())
            {
                throw new ConnectionException(Resources.AppResources.NetworkExceptionText);
            }

            using (var client = new HttpClient())
            {
                Uri rankingServerPage = new Uri(PHP_SERVER_PAGE_URL);

                var content = new HttpFormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("process", READ_PROCESS),
                    new KeyValuePair<string, string>("count", rankingLimit.ToString())
                });

                var jsonResult = await client.PostAsync(rankingServerPage, content);

                List<CustomerScore> result = JsonConvert.DeserializeObject<List<CustomerScore>>(jsonResult.Content.ToString());

                return result;
            }
        }

        public async Task<bool> AddRankingAsync(CustomerScore score)
        {
            if (!this.IsConnected())
            {
                throw new ConnectionException(Resources.AppResources.NetworkExceptionText);
            }
            else if(string.IsNullOrWhiteSpace(score.Name))
            {
                throw new ArgumentException("Les arguments sont invalides.");
            }

            using (var client = new HttpClient())
            {
                Uri rankingServerPage = new Uri(PHP_SERVER_PAGE_URL);

                var content = new HttpFormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("process", ADD_PROCESS),
                    new KeyValuePair<string, string>("name", score.Name),
                    new KeyValuePair<string, string>("points", score.Points.ToString()),
                });

                var jsonResult = await client.PostAsync(rankingServerPage, content);
                JObject obj = JObject.Parse(jsonResult.Content.ToString());

                JToken codeResult;
                if (obj.TryGetValue(CODE_PROPERTY, out codeResult))
                {
                    return ((string)codeResult) != REQUEST_CODE_ERROR;
                }
                else throw new NullReferenceException("Erreur lors de la reception des données.");
            }
        }
    }
}
