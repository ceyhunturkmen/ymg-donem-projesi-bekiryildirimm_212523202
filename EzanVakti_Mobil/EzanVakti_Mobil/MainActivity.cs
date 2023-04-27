using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using EzanVakti_Mobil.Resources;
using EzanVakti_Mobil.Resources.VeriTabani;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Essentials;
using static System.Net.Mime.MediaTypeNames;

namespace EzanVakti_Mobil
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        // Api Adresi--> https://api.aladhan.com/v1/calendar/2023/4?latitude=37.017448347669024&longitude=37.34085951963926&&month=4&year=2023&tune=0,0,-7,5,4,6,6,0
        CancellationTokenSource cts;

        List<namazVaktiData> data;
        veritabani veriTabani;
        DateTime bugun = DateTime.Now;
        namazVaktiData ezan;
        TextView text;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            text=FindViewById<TextView>(Resource.Id.AnaSayfaTarih);
         //   text.Text = "merhaba";
            DateTime dt = DateTime.Now;

            var location = await GetCurrentLocation();
            namazVaktiApi namazVakti = new namazVaktiApi(location.Latitude.ToString(), location.Longitude.ToString(), dt.Month, dt.Year);
              // FindViewById<TextView>(Resource.Id.AnaSayfaTarih).Text = namazVaktiApi.enlem + " " + namazVaktiApi.boylam + " " + namazVaktiApi.ay + " " + namazVaktiApi.yil;
             namazVakti.EzanSqlite();
            ezan = new namazVaktiData();
            data=new List<namazVaktiData>();
        //    text.Text= namazVakti.enlem + "\n" + namazVakti.boylam + "\n" + namazVakti.ay + "\n" + namazVakti.yil;
            //  text.Text = location.Latitude + " " + location.Longitude;
          //  text.Text = dt.Year.ToString();
              veriTabani = new veritabani();
              data = veriTabani.selectTable("NamazVakti.db");
       


                foreach (var item in data)
                {
                    if (item.GregDay == bugun.Day)
                    {
                        ezan = item;
                    }
                }
            
             text.Text = "  " + ezan.GregDay + "\n" + ezan.GregAylar + "\n" + ezan.GregYear;
            /*    FindViewById<Button>(Resource.Id.btnOk).Click += async delegate
                {
                    DateTime dt = DateTime.Now;

                    var location = await GetCurrentLocation();
                    namazVaktiApi namazVakti = new namazVaktiApi(location.Latitude.ToString(), location.Longitude.ToString(), dt.Month, dt.Year);
                 //   FindViewById<TextView>(Resource.Id.txtvw1).Text = namazVaktiApi.enlem + " " + namazVaktiApi.boylam + " " + namazVaktiApi.ay + " " + namazVaktiApi.yil;
                   // namazVakti.EzanSqlite();
                    SetContentView(Resource.Layout.activity_main);
                    text.Text = "merhaba";
                 //  text.Text= namazVaktiApi.enlem + " " + namazVaktiApi.boylam + " " + namazVaktiApi.ay + " " + namazVaktiApi.yil;
                    /*  veriTabani = new veritabani();
                      data = veriTabani.selectTable("NamazVakti.db");
                      foreach (var item in data)
                      {
                          if (item.GregDay == bugun.Day)
                          {
                              ezan = item;
                          }
                      }
                      FindViewById<TextView>(Resource.Id.txtvw1).Text = "  " + ezan.GregDay + "\n" + ezan.GregAylar + "\n" + ezan.GregYear;
                };*/
            // Set our view from the "main" layout resource
            // SetContentView(Resource.Layout.activity_main);
            /*      Intent intent = new Intent(this, typeof(GirişEkrani));
                  this.StartActivity(intent);*/


            /*
             veriTabani = new veritabani();
             data=veriTabani.selectTable("NamazVakti.db");
             foreach(var item in data)
             {
                 if(item.GregDay==bugun.Day)
                 {
                     ezan=item;
                 }
             }
             FindViewById<TextView>(Resource.Id.txtvw1).Text= "  " + ezan.GregDay + "\n" + ezan.GregAylar + "\n" + ezan.GregYear;*/

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        async Task<Location> GetCurrentLocation()
        {
 
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

        
                    return location;
                
 
            


        }
    }
}