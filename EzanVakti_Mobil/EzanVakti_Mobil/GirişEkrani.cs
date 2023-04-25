using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EzanVakti_Mobil.Resources;
using Java.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
namespace EzanVakti_Mobil
{
    [Activity(Label = "GirişEkrani")]
    public class GirişEkrani : Activity
    {
        CancellationTokenSource cts;
       
        public static string enlem;
        public static string boylam;
        public static int ay;
        public static int yil;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.first_screen);
            FindViewById<Button>(Resource.Id.btnOk).Click += async delegate
            {
                DateTime dt=DateTime.Now;
               
                var location =await GetCurrentLocation();
                namazVaktiApi namazVakti = new namazVaktiApi(location.Latitude.ToString(), location.Longitude.ToString(), dt.Month, dt.Year);
                namazVakti.EzanSqlite();
                SetContentView(Resource.Layout.activity_main);
            };
           
            // Create your application here
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        async Task<Location> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    return location;     
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                return null;
                // Unable to get location
            }
        }

       /* protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }*/
    }
}