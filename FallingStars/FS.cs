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
    class FS
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameType { get; set; }
        public string Recclass { get; set; }
        public string Mass { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Year { get; set; }
    }
}