using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EzanVakti_Mobil.Resources.EzanVakitleri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzanVakti_Mobil.Resources
{
    public class namazVaktiApi
    {
        public static string ApiLink(string enlem,string boylam,int yil,int ay)
        {
            return $"https://api.aladhan.com/v1/calendar?latitude={enlem}&longitude={boylam}&&month={ay}&year={yil}&tune=0,0,-7,5,4,6,6,0";
        }

        private async Task<EzanVakti> EzanApi()
        {
            // var ezanapi=ApiLink()
            return null;
        }
    }
}
// Api Adresi--> https://api.aladhan.com/v1/calendar/2023/4?latitude=37.017448347669024&longitude=37.34085951963926&&month=4&year=2023&tune=0,0,-7,5,4,6,6,0