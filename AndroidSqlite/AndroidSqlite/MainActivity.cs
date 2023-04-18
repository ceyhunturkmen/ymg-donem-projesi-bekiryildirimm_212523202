using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidSqlite.Resources;
using AndroidSqlite.Resources.DataHelper;
using AndroidSqlite.Resources.Model;
using AndroidX.AppCompat.App;
using System.Collections.Generic;

namespace AndroidSqlite
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ListView lstData;
        List<AndroidSqlite.Resources.Model.Person> lstSource = new List<Resources.Model.Person>();
         DataBase db;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            db = new DataBase();
            db.createDataBase();
            lstData = FindViewById<ListView>(Resource.Id.listView1);
            var intent = new Intent(this, typeof(Resource.Layout.list_view_dataTemplate));
            StartActivity(intent);
            var edtName = FindViewById<EditText>(Resource.Id.edtname);
            var edtAge = FindViewById<EditText>(Resource.Id.edtage);
            var edtCity = FindViewById<EditText>(Resource.Id.edtcity);

            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnDlt = FindViewById<Button>(Resource.Id.btnDlt);

            LoadData();
            btnAdd.Click += delegate
            {
                AndroidSqlite.Resources.Model.Person person = new Resources.Model.Person()
                {   
                    Name = edtName.Text,
                    Age =int.Parse(edtAge.Text),
                    City = edtCity.Text,
                };
                db.InsertIntoTablePerson(person);
                LoadData();
            };
            btnEdit.Click += delegate
            {
                Resources.Model.Person person = new Resources.Model.Person()
                {   
                    Id=int.Parse(edtName.Tag.ToString()),
                   // Id=ListViewAdapter.num,
                    
                    Name = edtName.Text,
                    Age = int.Parse(edtAge.Text),
                    City = edtCity.Text,
                };
                db.updateTablePerson(person);
                LoadData();
            };
           btnDlt.Click += delegate
            {
                AndroidSqlite.Resources.Model.Person person = new Resources.Model.Person()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    Age = int.Parse(edtAge.Text),
                    City = edtCity.Text,
                };
                db.deleteTablePerson(person);
                LoadData();
            };

            lstData.ItemClick += (s, e) =>
            {
                for (int i = 0; i < lstData.Count; i++)
                {
                    if (e.Position == i)
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.DarkGray);
                    else
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
                var txtName = e.View.FindViewById<TextView>(Resource.Id.txtName);
                var txtAge = e.View.FindViewById<TextView>(Resource.Id.txtAge);
                var txtCity = e.View.FindViewById<TextView>(Resource.Id.txtCity);
                var txtId = e.View.FindViewById<TextView>(Resource.Id.txtId);
                edtName.Text = txtName.Text;
                //+ " " + e.Id.ToString();

                edtName.Tag = e.Id;
                //edtName.Tag = int.Parse(txtId.Text);
                edtAge.Text = txtAge.Text;
                //+" "+edtName.Tag.ToString();
                edtCity.Text=txtCity.Text;
                //e.
            };
                }
        private void LoadData()
        {
            lstSource = db.selectTablePerson();
            var adapter = new ListViewAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}