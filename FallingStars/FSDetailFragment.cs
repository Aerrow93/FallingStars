using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft;
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
    class FSDetailFragment : Fragment
    {
        private FS _fs;

        private Activity activity;

        private TextView _nameText;
        private TextView _recclassText;
        private TextView _nameTypeText;
        private TextView _massText;
        private TextView _yearText;
        private TextView _fallText;
        private TextView _latText;
        private TextView _longText;

        ImageButton _mapImageButton;

        public override void OnAttach(Activity activity)
        {
            base.OnAttach (activity);
            this.activity = activity;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if(Arguments!=null && Arguments.ContainsKey("fs"))
            {
                string fsJson = Arguments.GetString("fs");
                _fs = JsonConvert.DeserializeObject<FS>(fsJson);
            } else
            {
                _fs = new FallingStars.FS();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.FSDetailFragment, container, false);

            _nameText = view.FindViewById<TextView>(Resource.Id.nameTextView);
            _recclassText = view.FindViewById<TextView>(Resource.Id.recclassTextView);
            _nameTypeText = view.FindViewById<TextView>(Resource.Id.nameTypeTextView);
            _massText = view.FindViewById<TextView>(Resource.Id.massTextView);
            _yearText = view.FindViewById<TextView>(Resource.Id.yearTextView);
            _fallText = view.FindViewById<TextView>(Resource.Id.fallTextView);
            _latText = view.FindViewById<TextView>(Resource.Id.latTextView);
            _longText = view.FindViewById<TextView>(Resource.Id.longTextView);

            UpdateUI();
            return view;
        }

        protected void UpdateUI()
        {
            _nameText.Text = _fs.Name;
            _recclassText.Text = _fs.Recclass;
            _nameTypeText.Text = _fs.NameType;
            _massText.Text = _fs.Mass;
            _yearText.Text = _fs.Year;
            _fallText.Text = _fs.Fall;
            _latText.Text = _fs.Latitude.ToString();
            _longText.Text = _fs.Longitude.ToString();
        }
    }
}