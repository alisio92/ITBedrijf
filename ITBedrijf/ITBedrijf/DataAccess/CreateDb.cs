using ITBedrijf.Models;
using ITBedrijfProject.DataAcces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;

namespace ITBedrijf.DataAccess
{
    public class CreateDb
    {
        public static void CreateDatabase(Organisation o)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string create = File.ReadAllText(path + "\\Data\\create.txt");
            string sql = create.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);
            foreach (string commandText in RemoveGo(sql))
            {
                Database.ModifyData(Database.GetConnection("AdminDB"), commandText);
            }
            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction("AdminDB");
                string fill = File.ReadAllText(path + "\\Data\\fill.txt");
                string sql2 = fill.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);

                foreach (string commandText in RemoveGo(sql2))
                {
                    Database.ModifyData(trans, commandText);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.Message);
            }
        }

        private static string[] RemoveGo(string input)
        {
            string[] splitter = new string[] { "\r\nGO\r\n" };
            string[] commandTexts = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            return commandTexts;
        }
    }
}