using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.Events.Models
{
    public record ConfigurationModel: BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Events.Enabled")]
        public bool EventsEnabled { get; set; }
        public bool EventsEnabled_OverrideForStore { get; set; }

      
    }
}
