using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ITBedrijf.DataAccess;

namespace ITBedrijf.Hubs
{
    public class Counters : Hub
    {
        public void GetAmountOrganisations()
        {
            int amountOrganisations = DAOrganisation.GetOrganisationsCount();
            Clients.All.NumberOffOrganisations(amountOrganisations);
        }

        public void GetAmountRegisters()
        {
            int amountRegisters = DARegister.GetRegistersCount();
            Clients.All.NumberOffRegisters(amountRegisters);
        }

        public void GetAmountLogs()
        {
            int amountLogs = DALog.GetErrorlogsCount();
            Clients.All.NumberOffLogs(amountLogs);
        }
    }
}