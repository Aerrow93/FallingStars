using System.Collections.Generic;
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

    }
}

