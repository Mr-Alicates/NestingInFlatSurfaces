using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Interfaces;

namespace GetLoadedPlugins
{
    [Export(typeof(IInterfacePlugin))]
    public class GetLoadedPluginsPlugin : IInterfacePlugin
    {
        ToolStripMenuItem menuItem = new ToolStripMenuItem("Loaded Plugins");

        public string PluginName
        {
            get { return "LoadedPlugins"; }
        }

        public void DeployPlugin(ICore applicationCore)
        {
            applicationCore.AddToPluginMenu(PluginName, menuItem);
        }

        public void AllPluginsDeployed(ICore applicationCore)
        {
            List<IInterfacePlugin> plugins = applicationCore.GetLoadedPlugins();

            foreach (IInterfacePlugin plugin in plugins)
            {
                menuItem.DropDownItems.Add(new ToolStripMenuItem(plugin.PluginName));
            }
        }
    }
}
