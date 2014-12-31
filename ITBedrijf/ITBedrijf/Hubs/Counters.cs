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
            int amountOrganisations = DAOrganisation.GetOrganisations().Count;
            Clients.All.NumberOffOrganisations(amountOrganisations);
        }

        public void GetAmountRegisters()
        {
            int amountRegisters = DARegister.GetRegisters().Count;
            Clients.All.NumberOffRegisters(amountRegisters);
        }

        public void GetAmountLogs()
        {
            int amountLogs= DALog.GetErrorlog().Count;
            Clients.All.NumberOffLogs(amountLogs);
        }
    }
}