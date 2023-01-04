using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Security;
using Nop.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.Events.Services
{
    public partial class EventsPermissionProvider : IPermissionProvider
    {
        //admin area permissions
        public static readonly PermissionRecord ManageEventsPlugin = new() { Name = "Connect Api. Manage Plugin", SystemName = "ManageEventsPlugin", Category = "Standard" };
        public static readonly PermissionRecord ManageEvents = new() { Name = "Connect Api.Connect to api ", SystemName = "ManageEventsItems", Category = "Standard" };


        public HashSet<(string systemRoleName, PermissionRecord[] permissions)> GetDefaultPermissions()
        {
            return new HashSet<(string, PermissionRecord[])>
            {
                (
                    NopCustomerDefaults.AdministratorsRoleName,
                    new[]
                    {
                        ManageEventsPlugin,
                        ManageEvents
                    }
                )
            };
        }

        public IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[]
            {
                ManageEventsPlugin,
                ManageEvents
            };
        }
    }
}
