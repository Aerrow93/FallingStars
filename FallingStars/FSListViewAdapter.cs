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

namespace FallingStars
{
    class FSListViewAdapter : BaseAdapter<FS>
    {
        private readonly Activity context;
        private List<FS> fsListData;

        public FSListViewAdapter (Activity _context, List<FS> _fsListData)
        {
            //:base(){
            //    this.context = _context;
            //    this.fsListData = _fsListData;
            //}
        }

        public override int Count
        {
            get
            {
                return fsListData.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position; 
        }

        public override FS this[int index]
        {
            get
            {
                return fsListData[index];
            }
        }
    } 
}