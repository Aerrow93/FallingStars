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
    [Activity (Label = "FSDetailActivity")]
    class FSDetailActivity : Activity
    {
        private FS _fs;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FSList);

            var detailFragment = new FSDetailFragment();
            detailFragment.Arguments = new Bundle();

            if (Intent.HasExtra("fs"))
            {
                string fsJson = Intent.GetStringExtra("fs");
                detailFragment.Arguments.PutString("fs", fsJson);
            }

            FragmentTransaction ft = FragmentManager.BeginTransaction();
            ft.Add(Resource.Id.fsDetailLayout, detailFragment);
            ft.Commit();

        }

    }
}