using ITBedrijf.PresentationModels;
using ITBedrijfProject.DataAcces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ITBedrijf.DataAccess
{
    public class DAOrganisationRegister
    {
        public static PMOrganisationRegister GetRegisterById(int id)
        {
            string sql = "SELECT * FROM Organisation_Register WHERE OrganisationID=@OrganisationID";
            DbParameter par1 = Database.AddParameter("AdminDB", "@OrganisationID", id);
            DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1);
            PMOrganisationRegister organisationRegister = new PMOrganisationRegister();
            while (reader.Read())
            {
                organisationRegister.OrganisationID = (int)reader["OrganisationID"];
                organisationRegister.RegisterID = (int)reader["RegisterID"];
                organisationRegister.FromDate = (DateTime)reader["FromDate"];
                organisationRegister.UntilDate = (DateTime)reader["UntilDate"];
            }
            return organisationRegister;
        }
    }
}