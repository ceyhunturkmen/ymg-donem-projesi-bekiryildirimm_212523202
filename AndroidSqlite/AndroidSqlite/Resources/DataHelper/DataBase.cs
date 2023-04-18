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
using Android.Util;
using AndroidSqlite.Resources.Model;

namespace AndroidSqlite.Resources.DataHelper
{
    public class DataBase
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool createDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.CreateTable<AndroidSqlite.Resources.Model.Person>();
                    
                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLiteEx", e.Message);
                return false;
            }
        }


        public bool InsertIntoTablePerson(AndroidSqlite.Resources.Model.Person person)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Insert(person);
                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLite Ex", e.Message);
                return false;
            }
        }

        public List<AndroidSqlite.Resources.Model.Person> selectTablePerson()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    return connection.Table<AndroidSqlite.Resources.Model.Person>().ToList();
                  
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLiteEx", e.Message);
                return null;
            }
        }

        public bool updateTablePerson(AndroidSqlite.Resources.Model.Person person)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Query<AndroidSqlite.Resources.Model.Person>("UPDATE Person set Name=?,Age=?,City=? where Id=?", person.Name, person.Age, person.City,person.Id);
                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLite Ex", e.Message);
                return false;
            }
        }

        public bool deleteTablePerson(AndroidSqlite.Resources.Model.Person person)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Delete(person);
                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLite Ex", e.Message);
                return false;
            }
        }

        public bool selectTablePerson(AndroidSqlite.Resources.Model.Person person)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Query<AndroidSqlite.Resources.Model.Person>("SELECT * from Person where Id=?",  person.Id);
                    return true;
                }
            }
            catch (SQLiteException e)
            {
                Log.Info("SQLite Ex", e.Message);
                return false;
            }
        }

    }
}