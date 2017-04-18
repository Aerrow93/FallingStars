using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Content.PM;
using Android.Content;

namespace FallingStarsApp
{
    [Activity(Label = "FallingStars", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class FSListActivity : Activity
    {
        public static bool isDualMode = false;

        private ListView fsListView;
        private ProgressBar progressBar;
        private List<FS> fsListData;
        private FSListViewAdapter fsListAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FSList);

            var detailsLayout = FindViewById(Resource.Id.fsDetailLayout);
            if (detailsLayout != null && detailsLayout.Visibility == ViewStates.Visible)
            {
                isDualMode = true;
            } else
            {
                isDualMode = false;
            }

            fsListView = FindViewById<ListView>(Resource.Id.fsListView);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);

            fsListView.ItemClick += FSClicked;
        }

        //public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        //{
        //    inflater.Inflate(Resource.Menu.FSListViewMenu, menu);
        //    base.OnCreateOptionsMenu(menu, inflater);
        //}

        public async void DownloadFSListAsync()
        {
            FSService service = new FSService();
            progressBar.Visibility = ViewStates.Visible;
            fsListData = await service.GetFSListAsync();
            progressBar.Visibility = ViewStates.Gone;

            fsListAdapter = new FSListViewAdapter(this, fsListData);
            fsListView.Adapter = fsListAdapter;
        }

        protected void FSClicked(object sender, ListView.ItemClickEventArgs e)
        {
            FS fs = result.fsListData[(int)e.Id];
            Console.Out.WriteLine("Falling Star Clicked: Name is {0}", fs.Name);

            Intent fsDetailIntent = new Intent(this, typeof(FSDetailActivity));
            string fsJson = JsonConvert.SerializeObject(fs);
            fsDetailIntent.PutExtra("fs", fsJson);
            StartActivity(fsDetailIntent);
        }
    }
}

