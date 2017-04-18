using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Android.Content;
using Android.Net;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace FallingStars
{
    class FSService
    {
        private const string GET_FS = "https://data.nasa.gov/resource/y77d-th95.json";
        private List<FS> fsListData = null;

        private ListView fsListView;
        private ProgressBar progressBar;
        private FSListViewAdapter fsListAdapter;

        public async Task<List<FS>> GetFSListAsync()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
            }
            else
            {
                Console.Out.WriteLine("Failed to fetch data. Try again later!");
                return null;
            }
        }

        public async void DoanloadFSListAsync()
        {
            FSService service = new FSService();

            if (!service.isConnected(this))
            {
                Toast toast = Toast.MakeText(this, "Not connected to internet. Please check your device network settings.",
                    ToastLength.Short);
                toast.Show();
            } else
            {
                progressBar.Visibility = ViewStates.Visible;
                fsListData = await service.GetFSListAsync();
                progressBar.Visibility = ViewStates.Gone;

                fsListAdapter = new FSListViewAdapter(this, fsListData);
                fsListView.Adapter = fsListAdapter;

            }
        }

        public bool isConnected(Context activity)
        {
            var connectivityManager = (ConnectivityManager)activity.GetSystemService(Context.ConnectivityService);
            var activeConnection = connectivityManager.ActiveNetworkInfo;
            return (null != activeConnection && activeConnection.IsConnected);
        }
    }
}