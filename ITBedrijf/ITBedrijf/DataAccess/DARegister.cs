using ITBedrijf.Models;
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
        public static List<Register> GetRegisters()
        {
            string sql = "SELECT * FROM Registers";
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql);
            List<Register> Registers = new List<Register>();
            while (reader.Read())
            {
                Register register = new Register();
                register.Id = (int)reader["ID"];
                register.RegisterName = reader["RegisterName"].ToString();
                register.Device = reader["Device"].ToString();
                register.PurchaseDate = (DateTime)reader["PurchaseDate"];
                register.ExpiresDate = (DateTime)reader["ExpiresDate"];
                Registers.Add(register);
            }
            return Registers;
        }
    }
}