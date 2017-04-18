using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android;

namespace FallingStarsApp
{
    class FSListViewAdapter : BaseAdapter<FS>
    {
        private readonly Activity context;
        private List<FS> fsListData;

        public FSListViewAdapter(Activity _context, List<FS> _fsListData)
            : base()
        {
            this.context = _context;
            this.fsListData = _fsListData;
        }

        public override int Count
        {
            get
            {
                return fsListData.Count;
            }
        }

        public override FS this[int index]
        {
            get
            {
                return fsListData[index];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.FSListItem, null);
            }

            FS fs = this[position];
            view.FindViewById<TextView>(Resource.Id.nameInfoTextView).Text = fs.Name;
            view.FindViewById<TextView>(Resource.Id.massInfoTextView).Text = fs.Mass;
            view.FindViewById<TextView>(Resource.Id.yearInfoTextView).Text = fs.Year;

            return view;
        }
    }
}