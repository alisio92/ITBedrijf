using ITBedrijf.Models;
using ITBedrijfProject.DataAcces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ITBedrijf.DataAccess
{
    public class DALog
    {
        //public static List<Errorlog> GetErrorlog()
        //{
        //    string sql = "SELECT Errorlog.RegisterID, Registers.RegisterName, Registers.Device, Errorlog.Timestamp, Errorlog.Message, Errorlog.Stacktrace";
        //    sql += " FROM Errorlog";
        //    sql += " INNER JOIN Registers";
        //    sql += " ON Errorlog.RegisterID=Registers.ID";
        //    DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql);
        //    List<Errorlog> Errorlog = null;
        //    if (reader != null)
        //    {
        //        Errorlog = new List<Errorlog>();
        //        int index = 0;
        //        while (reader.Read())
        //        {
        //            Errorlog errorlog = new Errorlog();
        //            errorlog.Index = index++;
        //            errorlog.RegisterID = (int)reader["RegisterID"];
        //            errorlog.RegisterName = errorlog.Message = reader["RegisterName"].ToString();
        //            errorlog.Device = errorlog.Message = reader["Device"].ToString();
        //            errorlog.Timestamp = (DateTime)reader["Timestamp"];
        //            errorlog.Message = reader["Message"].ToString();
        //            errorlog.Stacktrace = reader["Stacktrace"].ToString();

        //            Errorlog.Add(errorlog);
        //        }
        //    }
        //    return Errorlog;
        //}

        public static List<Errorlog> GetErrorlog(int offset, int aantal)
        {
            string sql = "SELECT Errorlog.RegisterID, Registers.RegisterName, Registers.Device, Errorlog.Timestamp, Errorlog.Message, Errorlog.Stacktrace";
            sql += " FROM Errorlog";
            sql += " INNER JOIN Registers";
            sql += " ON Errorlog.RegisterID=Registers.ID";
            sql += " ORDER BY Errorlog.RegisterID OFFSET @Offset ROWS FETCH NEXT @Aatal ROWS ONLY";
            DbParameter par1 = Database.AddParameter("AdminDB", "@Offset", offset * aantal);
            DbParameter par2 = Database.AddParameter("AdminDB", "@Aatal", aantal);
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1, par2);
            List<Errorlog> Errorlog = null;
            if (reader != null)
            {
                Errorlog = new List<Errorlog>();
                int index = 0;
                while (reader.Read())
                {
                    Errorlog errorlog = new Errorlog();
                    errorlog.Index = index++;
                    errorlog.RegisterID = (int)reader["RegisterID"];
                    errorlog.RegisterName = errorlog.Message = reader["RegisterName"].ToString();
                    errorlog.Device = errorlog.Message = reader["Device"].ToString();
                    errorlog.Timestamp = (DateTime)reader["Timestamp"];
                    errorlog.Message = reader["Message"].ToString();
                    errorlog.Stacktrace = reader["Stacktrace"].ToString();

                    Errorlog.Add(errorlog);
                }
            }
            return Errorlog;
        }

        public static int GetErrorlogsCount()
        {
            string sql = "SELECT count(*) as count FROM Errorlog";
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql);
            if (reader != null)
            {
                reader.Read();
                return (int)reader["count"];
            }
            return 0;
        }

        public static int GetErrorlogsCount(int id)
        {
            string sql = "SELECT count(*) as count FROM Errorlog WHERE RegisterID=@RegisterID";
            DbParameter par1 = Database.AddParameter("AdminDB", "@RegisterID", id);
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1);
            if (reader != null)
            {
                reader.Read();
                return (int)reader["count"];
            }
            return 0;
        }

        //public static List<Errorlog> GetLogsById(int id)
        //{
        //    string sql = "SELECT Errorlog.RegisterID, Registers.RegisterName, Registers.Device, Errorlog.Timestamp, Errorlog.Message, Errorlog.Stacktrace";
        //    sql += " FROM Errorlog";
        //    sql += " INNER JOIN Registers";
        //    sql += " ON Errorlog.RegisterID=Registers.ID";
        //    sql += " WHERE RegisterID=@ID";
        //    DbParameter par1 = Database.AddParameter("AdminDB", "@ID", id);
        //    DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1);
        //    List<Errorlog> Errorlog = null;
        //    if (reader != null)
        //    {
        //        Errorlog = new List<Errorlog>();
        //        int index = 0;
        //        while (reader.Read())
        //        {
        //            Errorlog errorlog = new Errorlog();
        //            errorlog.Index = index++;
        //            errorlog.RegisterID = (int)reader["RegisterID"];
        //            errorlog.RegisterName = errorlog.Message = reader["RegisterName"].ToString();
        //            errorlog.Device = errorlog.Message = reader["Device"].ToString();
        //            errorlog.Timestamp = (DateTime)reader["Timestamp"];
        //            errorlog.Message = reader["Message"].ToString();
        //            errorlog.Stacktrace = reader["Stacktrace"].ToString();

        //            Errorlog.Add(errorlog);
        //        }
        //    }
        //    return Errorlog;
        //}

        public static List<Errorlog> GetLogsById(int id, int offset, int aantal)
        {
            string sql = "SELECT Errorlog.RegisterID, Registers.RegisterName, Registers.Device, Errorlog.Timestamp, Errorlog.Message, Errorlog.Stacktrace";
            sql += " FROM Errorlog";
            sql += " INNER JOIN Registers";
            sql += " ON Errorlog.RegisterID=Registers.ID";
            sql += " WHERE RegisterID=@ID";
            sql += " ORDER BY Errorlog.RegisterID OFFSET @Offset ROWS FETCH NEXT @Aatal ROWS ONLY";
            DbParameter par1 = Database.AddParameter("AdminDB", "@ID", id);
            DbParameter par2 = Database.AddParameter("AdminDB", "@Offset", offset * aantal);
            DbParameter par3 = Database.AddParameter("AdminDB", "@Aatal", aantal);
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1, par2, par3);
            List<Errorlog> Errorlog = null;
            if (reader != null)
            {
                Errorlog = new List<Errorlog>();
                int index = 0;
                while (reader.Read())
                {
                    Errorlog errorlog = new Errorlog();
                    errorlog.Index = index++;
                    errorlog.RegisterID = (int)reader["RegisterID"];
                    errorlog.RegisterName = errorlog.Message = reader["RegisterName"].ToString();
                    errorlog.Device = errorlog.Message = reader["Device"].ToString();
                    errorlog.Timestamp = (DateTime)reader["Timestamp"];
                    errorlog.Message = reader["Message"].ToString();
                    errorlog.Stacktrace = reader["Stacktrace"].ToString();

                    Errorlog.Add(errorlog);
                }
            }
            return Errorlog;
        }
    }
}