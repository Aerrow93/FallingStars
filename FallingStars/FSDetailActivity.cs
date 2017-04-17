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

namespace FallingStars
{
    class FSDetailActivity
    {
        private FS _fs;

        private TextView _nameText;
        private TextView _recclassText;
        private TextView _nameTypeText;
        private TextView _massText;
        private TextView _yearText;
        private TextView _fallText;
        private TextView _latText;
        private TextView _longText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FSList);

            _nameText = FindViewById<TextView>(Resource.Id.nameTextView);
            _recclassText = FindViewById<TextView>(Resource.Id.recclassTextView);
            _nameTypeText = FindViewById<TextView>(Resource.Id.nameTypeTextView);
            _massText = FindViewById<TextView>(Resource.Id.massTextView);
            _yearText = FindViewById<TextView>(Resource.Id.yearTextView);
            _fallText = FindViewById<TextView>(Resource.Id.fallTextView);
            _latText = FindViewById<TextView>(Resource.Id.latTextView);
            _longText = FindViewById<TextView>(Resource.Id.longTextView);

            if (Intent.HasExtra("fs"))
            {
                string fsJson = Intent.GetStringExtra("fs");
                _fs = JsonConvert.DeserializeObject<FS>(fsJson);
            } else
            {
                _fs = new FS();
            }

            UpdateUI();
        }

        protected void UpdateUI()
        {
            _nameText.Text = _fs.Name;
            _recclassText.Text = _fs.Recclasee;
            _nameTypeText.Text = _fs.NameType;
            _massText.Text = _fs.Mass;
            _yearText.Text = _fs.Year;
            _fallText.Text = _fs.Fall;
            _latText.Text = _fs.Latitude;
            _longText.Text = _fs.Longitude;
        }
    }
}