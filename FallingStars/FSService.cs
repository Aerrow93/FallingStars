using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using Android.Content;
using Android.Net;


namespace FallingStars
{
    class FSService
    {
        private const string GET_FS = "https://data.nasa.gov/resource/y77d-th95.json";
        private List<FS> fsListData = null;

        public async Task<List<FS>> GetFSListAsync()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequesetHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.GetAsync(GET_FS);
            if (response != null || response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.Out.WriteLine("Response Body: \r\n{0}", content);

                JObject jsonResponse = JObject.Parse(content);
                IList<JToken> results = jsonResponse["fs"].ToList();
                foreach (JToken token in results)
                {
                    FS fs = token.ToObject<FS>();
                    fsListData.Add(fs);
                }
                return fsListData;
            } else
            {
                Console.Out.WriteLine("Failed to fetch data. Try again later!");
                return null;
            }

            


        }
    }
}