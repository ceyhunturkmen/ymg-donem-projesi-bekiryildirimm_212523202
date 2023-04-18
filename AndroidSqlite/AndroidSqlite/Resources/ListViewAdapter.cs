using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Net.IpSec.Ike;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroidSqlite.Resources
{   
    public class ViewHolder:Java.Lang.Object
    {
        public TextView txtName { get; set; }
        public TextView txtAge { get; set; }
        public TextView txtCity { get; set; }
        
    }
    public class ListViewAdapter:BaseAdapter
    {
        private Activity activity;
        private List<Resources.Model.Person> lstPerson;
       // public static int num;
        public ListViewAdapter(Activity activity, List<Resources.Model.Person> lstPerson)
        {
            this.activity = activity;
            this.lstPerson= lstPerson;
        }

        public override int Count
        {
            get { return lstPerson.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstPerson[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView??activity.LayoutInflater.Inflate(Resource.Layout.list_view_dataTemplate,parent,false);
            var txtName = view.FindViewById<TextView>(Resource.Id.txtName);
            var txtAge = view.FindViewById<TextView>(Resource.Id.txtAge);
            var txtCity = view.FindViewById<TextView>(Resource.Id.txtCity);
            var txtId = view.FindViewById<TextView>(Resource.Id.txtId);
            txtName.Text =lstPerson[position].Name;
            txtAge.Text = "" + lstPerson[position].Age;
            txtCity.Text = lstPerson[position].City;
           txtId.Text= lstPerson[position].Id.ToString();
            return view;
        }
    }
}