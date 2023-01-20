using System;
using SQLite;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;

namespace Olis.Models
{
    public class DB
    {
        public DB()
        {
        }
        private static string DBName = "recipe.db";
        public static SQLiteConnection conn;
        public static void OpenConnection()
        {
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, DBName);
            conn = new SQLiteConnection(fname);
            conn.CreateTable<RecipeBody>();
            conn.CreateTable<Recipe>();
            conn.CreateTable<Nutrition>();
        }
        public static void DeleteTableContents(string tableName)
        {
            int v = conn.Execute("DELETE FROM " + tableName);
        }

        public static void ResetAllTables()
        {
            int v = conn.Execute("DELETE FROM RecipeBody");
            int p = conn.Execute("DELETE FROM Recipe");
            int n = conn.Execute("DELETE FROM Nutrition");
        }
    }
}


