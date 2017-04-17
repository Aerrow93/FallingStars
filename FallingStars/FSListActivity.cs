﻿using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using Android.App;
using Android.Widget;
using Android.OS;

using Android.Views;

namespace FallingStars
{
    [Activity(Label = "FallingStars", MainLauncher = true, Icon = "@drawable/icon")]
    public class FSListActivity : Activity
    {
        private ListView fsListView;
        private ProgressBar progressBar;
        private List<FS> fsListData;
        private FSListViewAdapter fsListAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FSList);

            fsListView = FindViewById<ListView>(Resource.Id.fsListView);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);

            fsListView.ItemClick += FSClicked;
        }

        protected override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.FSListViewMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

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
            FSClicked fs = result.fsListData[(int)e.Id];
            Console.Out.WriteLine("Falling Star Clicked: Name is {0}", fs.Name);

            Intent fsDetailIntent = new Intent(this, typeof(FSDetailActivity));
            string fsJson = JsonConvert.SerializeObject(fs);
            fsDetailIntent.PutExtra("fs", fsJson);
            StartActivity(fsDetailIntent);
        }
    }
}

