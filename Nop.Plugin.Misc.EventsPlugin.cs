using Nop.Core;
using Nop.Plugin.Misc.Events.Models;
using Nop.Plugin.Misc.Events.Services;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.Events
{
    /// <summary>
    /// Rename this file and change to the correct type
    /// </summary>
    public class EventsPlugin : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {
        #region Fields
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly IPermissionService _permissionService;
        private readonly IPermissionProvider _permissionProvider;
        private readonly IPluginManager<EventsPlugin> _pluginManager;

        #endregion
        public EventsPlugin(
            ISettingService settingService,
            ILocalizationService localizationService,
            IWebHelper webHelper,
            IPermissionService permissionService,
            IPluginManager<EventsPlugin> pluginManager
            )
        {
            _settingService = settingService;
            _localizationService = localizationService;
            _webHelper = webHelper;
            _permissionService = permissionService;
            _permissionProvider = new EventsPermissionProvider();
            _pluginManager = pluginManager;

        }
        public override async Task InstallAsync()
        {
            //settings
            var settings = new EventsSettings()
            {
                EventsEnabled = true
            };

            await _settingService.SaveSettingAsync(settings);

            //locales
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Events.Enabled"] = "Events Enabled",
              
                //menu 
                ["Events.Events"] = "Events",
              

              

            });


            // permissions
            await _permissionService.InstallPermissionsAsync(_permissionProvider);

            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await _settingService.DeleteSettingAsync<EventsSettings>();

            await _localizationService.DeleteLocaleResourcesAsync("Events");

            // permissions
            await _permissionService.UninstallPermissionsAsync(_permissionProvider);
            await base.UninstallAsync();
        }

        public override Task UpdateAsync(string currentVersion, string targetVersion)
        {
            return base.UpdateAsync(currentVersion, targetVersion);
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/Events/Configure";
        }

        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            var pl = _pluginManager.LoadAllPluginsAsync();

            if (!_pluginManager.IsPluginActive(this, new List<string> { EventsDefaults.PluginSystemName }))
                return;

            if (
                !await _permissionService.AuthorizeAsync(EventsPermissionProvider.ManageEventsPlugin) &&
                !await _permissionService.AuthorizeAsync(EventsPermissionProvider.ManageEvents))
                return;

           
        }


    }
}
