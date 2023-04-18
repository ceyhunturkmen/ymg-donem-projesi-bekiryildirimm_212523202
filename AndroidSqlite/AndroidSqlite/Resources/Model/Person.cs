using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
namespace AndroidSqlite.Resources.Model
{
    [Table("Person")]
    public class Person
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public string City { get; set; }
    }
}