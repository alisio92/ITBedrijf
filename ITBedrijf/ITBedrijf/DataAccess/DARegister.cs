using ITBedrijf.Models;
using ITBedrijf.PresentationModels;
using ITBedrijfProject.DataAcces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ITBedrijf.DataAccess
{
    public class DARegister
    {
        //public static List<Register> GetRegisters()
        //{
        //    string sql = "SELECT ID, RegisterName, Device, PurchaseDate, ExpireDate FROM Registers";
        //    DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql);
        //    List<Register> Registers = null;
        //    if (reader != null)
        //    {
        //        Registers = new List<Register>();
        //        int index = 0;
        //        while (reader.Read())
        //        {
        //            Register register = new Register();
        //            register.Index = index++;
        //            register.Id = (int)reader["ID"];
        //            register.RegisterName = reader["RegisterName"].ToString();
        //            register.Device = reader["Device"].ToString();
        //            register.PurchaseDate = (DateTime)reader["PurchaseDate"];
        //            register.ExpireDate = (DateTime)reader["ExpireDate"];
        //            Registers.Add(register);
        //        }
        //    }
            
        //    return Registers;
        //}

        public static List<Register> GetRegisters(int offset, int aantal)
        {
            string sql = "SELECT ID, RegisterName, Device, PurchaseDate, ExpireDate FROM Registers ORDER BY ID OFFSET @Offset ROWS FETCH NEXT @Aatal ROWS ONLY";
            DbParameter par1 = Database.AddParameter("AdminDB", "@Offset", offset * aantal);
            DbParameter par2 = Database.AddParameter("AdminDB", "@Aatal", aantal);
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1, par2);
            List<Register> Registers = null;
            if (reader != null)
            {
                Registers = new List<Register>();
                int index = 0;
                while (reader.Read())
                {
                    Register register = new Register();
                    register.Index = index++;
                    register.Id = (int)reader["ID"];
                    register.RegisterName = reader["RegisterName"].ToString();
                    register.Device = reader["Device"].ToString();
                    register.PurchaseDate = (DateTime)reader["PurchaseDate"];
                    register.ExpireDate = (DateTime)reader["ExpireDate"];
                    Registers.Add(register);
                }
            }

            return Registers;
        }

        public static int GetRegistersCount()
        {
            string sql = "SELECT count(*) as count FROM Registers";
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql);
            if (reader != null)
            {
                reader.Read();
                return (int)reader["count"];
            }
            return 0;
        }

        public static int InsertRegister(Register register)
        {
            string sql = "INSERT INTO Registers VALUES(@RegisterName,@Device,@PurchaseDate,@ExpireDate)";
            DbParameter par1 = Database.AddParameter("AdminDB", "@RegisterName", register.RegisterName);
            DbParameter par2 = Database.AddParameter("AdminDB", "@Device", register.Device);
            DbParameter par3 = Database.AddParameter("AdminDB", "@PurchaseDate", register.PurchaseDate);
            DbParameter par4 = Database.AddParameter("AdminDB", "@ExpireDate", register.ExpireDate);
            return Database.InsertData(Database.GetConnection("AdminDB"), sql, par1, par2, par3, par4);
        }
    }
}