﻿using ITBedrijf.Extra;
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
        //public static List<Organisation> GetOrganisations()
        //{
        //    string sql = "SELECT ID, Login, Password, DbName, DbLogin, DbPassword, OrganisationName, Address, Email, Phone FROM Organisations";
        //    DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql);
        //    List<Organisation> Organisations = null;
        //    if (reader != null)
        //    {
        //        Organisations = new List<Organisation>();
        //        int index = 0;
        //        while (reader.Read())
        //        {
        //            Organisation organisation = new Organisation();
        //            organisation.Index = index++;
        //            organisation.Id = (int)reader["ID"];
        //            organisation.Login = reader["Login"].ToString();
        //            organisation.Password = reader["Password"].ToString();
        //            organisation.DbName = reader["DbName"].ToString();
        //            organisation.DbLogin = reader["DbLogin"].ToString();
        //            organisation.DbPassword = reader["DbPassword"].ToString();
        //            organisation.OrganisationName = reader["OrganisationName"].ToString();
        //            organisation.Address = reader["Address"].ToString();
        //            organisation.Email = reader["Email"].ToString();
        //            organisation.Phone = reader["Phone"].ToString();

        //            Organisations.Add(organisation);
        //        }
        //    }
        //    return Organisations;
        //}

        public static int GetOrganisationsCount()
        {
            string sql = "SELECT count(*) as count FROM Organisations";
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql);
            if (reader != null)
            {
                reader.Read();
                return (int)reader["count"];
            }
            return 0;
        }

        public static List<Organisation> GetOrganisations(int offset, int aantal)
        {
            string sql = "SELECT ID, Login, Password, DbName, DbLogin, DbPassword, OrganisationName, Address, Email, Phone FROM Organisations ORDER BY ID OFFSET @Offset ROWS FETCH NEXT @Aatal ROWS ONLY";
            DbParameter par1 = Database.AddParameter("AdminDB", "@Offset", offset * aantal);
            DbParameter par2 = Database.AddParameter("AdminDB", "@Aatal", aantal);
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1, par2);
            List<Organisation> Organisations = null;
            if (reader != null)
            {
                Organisations = new List<Organisation>();
                int index = 0;
                while (reader.Read())
                {
                    Organisation organisation = new Organisation();
                    organisation.Index = index++;
                    organisation.Id = (int)reader["ID"];
                    organisation.Login = reader["Login"].ToString();
                    string decryptedPassword = Cryptography.Decrypt(reader["Password"].ToString());
                    organisation.Password = decryptedPassword;
                    organisation.DbName = reader["DbName"].ToString();
                    organisation.DbLogin = reader["DbLogin"].ToString();
                    string decryptedDBPassword = Cryptography.Decrypt(reader["DbPassword"].ToString());
                    organisation.DbPassword = decryptedDBPassword;
                    organisation.OrganisationName = reader["OrganisationName"].ToString();
                    organisation.Address = reader["Address"].ToString();
                    organisation.Email = reader["Email"].ToString();
                    organisation.Phone = reader["Phone"].ToString();

                    Organisations.Add(organisation);
                }
            }
            return Organisations;
        }

        public static Organisation GetOrganisationsById(int id)
        {
            string sql = "SELECT ID, Login, Password, DbName, DbLogin, DbPassword, OrganisationName, Address, Email, Phone FROM Organisations WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("AdminDB", "@ID", id);
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1);
            Organisation organisation = new Organisation();
            if (reader != null)
            {
                int index = 0;
                while (reader.Read())
                {
                    organisation.Index = index++;
                    organisation.Id = (int)reader["ID"];
                    organisation.Login = reader["Login"].ToString();
                    string decryptedPassword = Cryptography.Decrypt(reader["Password"].ToString());
                    organisation.Password = decryptedPassword;
                    organisation.DbName = reader["DbName"].ToString();
                    organisation.DbLogin = reader["DbLogin"].ToString();
                    string decryptedDBPassword = Cryptography.Decrypt(reader["DbPassword"].ToString());
                    organisation.DbPassword = decryptedDBPassword;
                    organisation.OrganisationName = reader["OrganisationName"].ToString();
                    organisation.Address = reader["Address"].ToString();
                    organisation.Email = reader["Email"].ToString();
                    organisation.Phone = reader["Phone"].ToString();
                }
            }
            return organisation;
        }

        //public static List<Organisation> GetOrganisations()
        //{
        //    int id = 0;
        //    Organisation organisationRow = GetOrganisationsById(3);
        //    string sql = "SELECT TOP 4 ID, Login, Password, DbName, DbLogin, DbPassword, OrganisationName, Address, Email, Phone FROM Organisations";
        //    sql += " where (OrganisationName=@OrganisationName and Login=@Login and Address > @Address) OR (OrganisationName=@OrganisationName and Login > @Login) OR (OrganisationName > @OrganisationName)";
        //    sql += " ORDER BY OrganisationName, Login, Address";
        //    DbParameter par1 = Database.AddParameter("AdminDB", "@OrganisationName", organisationRow.OrganisationName);
        //    DbParameter par2 = Database.AddParameter("AdminDB", "@Login", organisationRow.Login);
        //    DbParameter par3 = Database.AddParameter("AdminDB", "@Address", organisationRow.Address);
        //    DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1, par2, par3);
        //    List<Organisation> Organisations = null;
        //    if (reader != null)
        //    {
        //        Organisations = new List<Organisation>();
        //        int index = 0;
        //        while (reader.Read())
        //        {
        //            Organisation organisation = new Organisation();
        //            organisation.Index = index++;
        //            organisation.Id = (int)reader["ID"];
        //            organisation.Login = reader["Login"].ToString();
        //            organisation.Password = reader["Password"].ToString();
        //            organisation.DbName = reader["DbName"].ToString();
        //            organisation.DbLogin = reader["DbLogin"].ToString();
        //            organisation.DbPassword = reader["DbPassword"].ToString();
        //            organisation.OrganisationName = reader["OrganisationName"].ToString();
        //            organisation.Address = reader["Address"].ToString();
        //            organisation.Email = reader["Email"].ToString();
        //            organisation.Phone = reader["Phone"].ToString();

        //            Organisations.Add(organisation);
        //        }
        //    }
        //    return Organisations;
        //}

        public static int InsertOrganisation(Organisation organisation)
        {
            int id = -1;
            if (CreateDb.CreateDatabase(organisation))
            {
                string sql = "INSERT INTO Organisations VALUES(@Login,@Password,@DbName,@DbLogin,@DbPassword,@OrganisationName,@Address,@Email,@Phone)";
                DbParameter par1 = Database.AddParameter("AdminDB", "@Login", organisation.Login);
                string encryptedPassword = Cryptography.Encrypt(organisation.Password);
                DbParameter par2 = Database.AddParameter("AdminDB", "@Password", encryptedPassword);
                DbParameter par3 = Database.AddParameter("AdminDB", "@DbName", organisation.DbName);
                DbParameter par4 = Database.AddParameter("AdminDB", "@DbLogin", organisation.DbLogin);
                string encryptedDBPassword = Cryptography.Encrypt(organisation.DbPassword);
                DbParameter par5 = Database.AddParameter("AdminDB", "@DbPassword", encryptedDBPassword);
                DbParameter par6 = Database.AddParameter("AdminDB", "@OrganisationName", organisation.OrganisationName);
                DbParameter par7 = Database.AddParameter("AdminDB", "@Address", organisation.Address);
                DbParameter par8 = Database.AddParameter("AdminDB", "@Email", organisation.Email);
                DbParameter par9 = Database.AddParameter("AdminDB", "@Phone", organisation.Phone);

                id = Database.InsertData(Database.GetConnection("AdminDB"), sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);
            }
            return id;
        }

        public static PMOrganisation GetOrganisationById(int id)
        {
            string sql = "SELECT ID, Login, Password, DbName, DbLogin, DbPassword, OrganisationName, Address, Email, Phone FROM Organisations WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("AdminDB", "@ID", id);
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1);
            PMOrganisation organisation = null;
            if (reader != null)
            {
                organisation = new PMOrganisation();
                while (reader.Read())
                {
                    organisation.Id = (int)reader["ID"];
                    organisation.Login = reader["Login"].ToString();
                    string decryptedPassword = Cryptography.Decrypt(reader["Password"].ToString());
                    organisation.Password = decryptedPassword;
                    organisation.DbName = reader["DbName"].ToString();
                    organisation.DbLogin = reader["DbLogin"].ToString();
                    string decryptedDBPassword = Cryptography.Decrypt(reader["DbPassword"].ToString());
                    organisation.DbPassword = decryptedDBPassword;
                    organisation.OrganisationName = reader["OrganisationName"].ToString();
                    organisation.Address = reader["Address"].ToString();
                    organisation.Email = reader["Email"].ToString();
                    organisation.Phone = reader["Phone"].ToString();
                }
            }
            return organisation;
        }

        public static int UpdateOrganisation(int id, Organisation organisation)
        {
            string sql = "UPDATE Organisations SET OrganisationName=@OrganisationName, Login=@Login, Password=@Password, DbName=@DbName, DbLogin=@DbLogin, DbPassword=@DbPassword, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("AdminDB", "@ID", id);
            DbParameter par2 = Database.AddParameter("AdminDB", "@OrganisationName", organisation.OrganisationName);
            DbParameter par3 = Database.AddParameter("AdminDB", "@Login", organisation.Login);
            string encryptedPassword = Cryptography.Encrypt(organisation.Password);
            DbParameter par4 = Database.AddParameter("AdminDB", "@Password", encryptedPassword);
            DbParameter par5 = Database.AddParameter("AdminDB", "@DbName", organisation.DbName);
            DbParameter par6 = Database.AddParameter("AdminDB", "@DbLogin", organisation.DbLogin);
            string encryptedDBPassword = Cryptography.Encrypt(organisation.DbPassword);
            DbParameter par7 = Database.AddParameter("AdminDB", "@DbPassword", encryptedDBPassword);
            DbParameter par8 = Database.AddParameter("AdminDB", "@Address", organisation.Address);
            DbParameter par9 = Database.AddParameter("AdminDB", "@Email", organisation.Email);
            DbParameter par10 = Database.AddParameter("AdminDB", "@Phone", organisation.Phone);

            return Database.ModifyData(Database.GetConnection("AdminDB"), sql, par1, par2, par3, par4, par5, par6, par7, par8, par9, par10);
        }
    }
}