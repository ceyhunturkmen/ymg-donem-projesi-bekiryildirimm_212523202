using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System;
using Picasso.Services;
using Android.Widget;
using static System.Net.Mime.MediaTypeNames;
using EzanVakti_Mobil;

namespace KıblePusulası_Mobil
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        CancellationTokenSource cts;
        AppCompatImageView a,b,c;
        AppCompatTextView txt1;
        double derece;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_pusula);
            a = FindViewById<AppCompatImageView>(Resource.Id.compassRlytCompassTop);
            b = FindViewById<AppCompatImageView>(Resource.Id.compassRlytCompassArrow);
            txt1 = FindViewById<AppCompatTextView>(Resource.Id.compassTvDegree);
          //  var location = await GetCurrentLocation();
            //var res = await ApiKible(location.Latitude.ToString(), location.Longitude.ToString());
            //derece = res.data.direction;
            Compass.Start(SensorSpeed.Game);
            Compass.ReadingChanged += pusula;
         //  b.Rotation = (360 - 171);
        }
        public void pusula(object sender, CompassChangedEventArgs e)
        {   
            var dt = e.Reading;
            a.Rotation =(360-(float)dt.HeadingMagneticNorth);
            b.Rotation = (360 - (float)dt.HeadingMagneticNorth+(float)SplashActivity1.derece);
            txt1.Text = Convert.ToInt32(dt.HeadingMagneticNorth).ToString();

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

                return location;

            }

            catch (Exception ex)
            {
                return null;
                // Unable to get location
            }
        }
        public static async Task<Picasso.Kible.Root> ApiKible(string lat, string lgt)
        {
            var KibleApi = $"https://api.aladhan.com/v1/qibla/{lat}/{lgt}";
            var httpService = new CLientModel();
            var result = await httpService.ProcessApi<Picasso.Kible.Root>(KibleApi);
            return result;
        }
    }
}