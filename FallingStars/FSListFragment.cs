using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FallingStars
{
    class FSListFragment : ListFragment
    {
        private ProgressBar progressBar;
        private List<FS> fsListData;
        private FSListViewAdapter fsListAdapter;
        private Activity activity;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.FSListFragment, container, false);
            progressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
            return view;
        }

        public override void OnAttach(Activity activity)
        {
            base.OnAttach (activity);
            this.activity = activity;
        }

        public async void DownloadFSListAsync()
        {
            FSService service = new FallingStars.FSService();
            if (!service.isConnected(activity))
            {
                Toast toast = Toast.MakeText(activity, "Not connected to internet. Please check your device network settings.", ToastLength.Short);
            } else
            {
                progressBar.Visibility = ViewStates.Visible;
                fsListData = await service.GetFSListAsync();
                progressBar.Visibility = ViewStates.Gone;

                fsListAdapter = new FSListViewAdapter(activity, fsListData);
                this.ListAdapter = fsListAdapter;
            }
        }

        public override void OnResume()
        {
            DownloadFSListAsync();
            base.OnResume();
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.FSListViewMenu, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actionRefresh:
                    DownloadFSListAsync();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            FS fs = fsListData[position];
            Intent fsDetailIntent = new Intent(activity, typeof(FSDetailActivity));
            string fsJson = JsonConvert.SerializeObject(fs);
            fsDetailIntent.PutExtra("fs", fsJson);
            StartActivity(fsDetailIntent);
        }
    }
}