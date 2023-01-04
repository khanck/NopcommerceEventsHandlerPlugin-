using Nop.Core.Configuration;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.Events.Models
{
    public class EventsSettings: ISettings
    {
        [NopResourceDisplayName("Events.Enabled")]
        public bool EventsEnabled { get; set; }
       
    }
}
