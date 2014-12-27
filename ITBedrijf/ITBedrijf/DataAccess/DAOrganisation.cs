using ITBedrijf.Models;
using ITBedrijf.PresentationModels;
using ITBedrijfProject.DataAcces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ITBedrijf.DataAccess
{
    public class DAOrganisation
    {
        public static List<Organisation> GetOrganisations()
        {
            string sql = "SELECT * FROM Organisation";
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql);
            List<Organisation> Organisations = new List<Organisation>();
            while (reader.Read())
            {
                Organisation organisation = new Organisation();
                organisation.Id = (int)reader["ID"];
                organisation.Login = reader["Login"].ToString();
                organisation.Password = reader["Password"].ToString();
                organisation.DbName = reader["DbName"].ToString();
                organisation.DbLogin = reader["DbLogin"].ToString();
                organisation.DbPassword = reader["DbPassword"].ToString();
                organisation.OrganisationName = reader["OrganisationName"].ToString();
                organisation.Address = reader["Address"].ToString();
                organisation.Email = reader["Email"].ToString();
                organisation.Phone = reader["Phone"].ToString();

                Organisations.Add(organisation);
            }
            return Organisations;
        }

        public static int InsertOrganisation(Organisation organisation)
        {
            string sql = "INSERT INTO Organisation VALUES(@Login,@Password,@DbName,@DbLogin,@DbPassword,@OrganisationName,@Address,@Email,@Phone)";
            DbParameter par1 = Database.AddParameter("AdminDB", "@Login", organisation.Login);
            DbParameter par2 = Database.AddParameter("AdminDB", "@Password", organisation.Password);
            DbParameter par3 = Database.AddParameter("AdminDB", "@DbName", organisation.DbName);
            DbParameter par4 = Database.AddParameter("AdminDB", "@DbLogin", organisation.DbLogin);
            DbParameter par5 = Database.AddParameter("AdminDB", "@DbPassword", organisation.DbPassword);
            DbParameter par6 = Database.AddParameter("AdminDB", "@OrganisationName", organisation.OrganisationName);
            DbParameter par7 = Database.AddParameter("AdminDB", "@Address", organisation.Address);
            DbParameter par8 = Database.AddParameter("AdminDB", "@Email", organisation.Email);
            DbParameter par9 = Database.AddParameter("AdminDB", "@Phone", organisation.Phone);

            int id = Database.InsertData(Database.GetConnection("AdminDB"), sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);
            CreateDb.CreateDatabase(organisation);
            return 0;
        }

        public static PMOrganisation GetOrganisationById(int id)
        {
            string sql = "SELECT * FROM Organisation WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("AdminDB", "@ID", id);
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1);
            PMOrganisation organisation = new PMOrganisation();
            while (reader.Read())
            {
                organisation.Id = (int)reader["ID"];
                organisation.Login = reader["Login"].ToString();
                organisation.Password = reader["Password"].ToString();
                organisation.DbName = reader["DbName"].ToString();
                organisation.DbLogin = reader["DbLogin"].ToString();
                organisation.DbPassword = reader["DbPassword"].ToString();
                organisation.OrganisationName = reader["OrganisationName"].ToString();
                organisation.Address = reader["Address"].ToString();
                organisation.Email = reader["Email"].ToString();
                organisation.Phone = reader["Phone"].ToString();
            }
            return organisation;
        }

        public static int UpdateOrganisation(int id, string Login, string Password, string DbName, string DbLogin, string DbPassword, string OrganisationName, string Address, string Email, string Phone)
        {
            string sql = "UPDATE Organisation SET OrganisationName=@OrganisationName, Login=@Login, Password=@Password, DbName=@DbName, DbLogin=@DbLogin, DbPassword=@DbPassword, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("AdminDB", "@ID", id);
            DbParameter par2 = Database.AddParameter("AdminDB", "@OrganisationName", OrganisationName);
            DbParameter par3 = Database.AddParameter("AdminDB", "@Login", Login);
            DbParameter par4 = Database.AddParameter("AdminDB", "@Password", Password);
            DbParameter par5 = Database.AddParameter("AdminDB", "@DbName", DbName);
            DbParameter par6 = Database.AddParameter("AdminDB", "@DbLogin", DbLogin);
            DbParameter par7 = Database.AddParameter("AdminDB", "@DbPassword", DbPassword);
            DbParameter par8 = Database.AddParameter("AdminDB", "@Address", Address);
            DbParameter par9 = Database.AddParameter("AdminDB", "@Email", Email);
            DbParameter par10 = Database.AddParameter("AdminDB", "@Phone", Phone);

            return Database.ModifyData(Database.GetConnection("AdminDB"), sql, par1, par2, par3, par4, par5, par6, par7, par8, par9, par10);
        }
    }
}