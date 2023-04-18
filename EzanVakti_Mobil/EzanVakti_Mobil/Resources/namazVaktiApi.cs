using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EzanVakti_Mobil.Resources.EzanVakitleri;
using Picasso.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzanVakti_Mobil.Resources
{
    public class namazVaktiApi
    {
        private static string enlem;
        private static string boylam;
        private static int ay;
        private static int yil;
        
        public namazVaktiApi()
        {

        }
        public namazVaktiApi(string enlem,string boylam,int ay, int yil)
        {
            enlem = enlem;
            boylam = boylam;
            ay = ay;
            yil = yil;
        }
        public namazVaktiApi(string enlem,string boylam)
        {
            enlem = enlem;
            boylam = boylam;
        }
        public static string ApiLink(string enlem,string boylam,int yil,int ay)
        {
            return $"https://api.aladhan.com/v1/calendar?latitude={enlem}&longitude={boylam}&&month={ay}&year={yil}&tune=0,0,-7,5,4,6,6,0";
        }

        private async Task<EzanVakti> EzanApi()
        {
            var ezanapi = ApiLink(enlem, boylam, yil, ay);
            var httpService = new CLientModel();
            var result = await httpService.ProcessApi<EzanVakti>(ezanapi);
            return result;
        }
    }
}
// Api Adresi--> https://api.aladhan.com/v1/calendar/2023/4?latitude=37.017448347669024&longitude=37.34085951963926&&month=4&year=2023&tune=0,0,-7,5,4,6,6,0