using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using System.ComponentModel.Composition;

namespace Core.Plugins
{
    public class PluginsLoader
    {
        public List<IInterfacePlugin> LoadedPlugins; 

        public void LoadPlugins(ICore core, string path)
        {
            if (string.IsNullOrEmpty(path) || ! Directory.Exists(path))
            {
                return;
            }

            LoadedPlugins = LoadPlugins(path);

            foreach (IInterfacePlugin plugin in LoadedPlugins)
            {
                plugin.DeployPlugin(core);
            }

            foreach (IInterfacePlugin plugin in LoadedPlugins)
            {
                plugin.AllPluginsDeployed(core);
            }

            return;
        }

        private static List<IInterfacePlugin> LoadPlugins(string path)
        {
            //recursive search

            List<IInterfacePlugin> loadedPlugins = new List<IInterfacePlugin>();

            foreach (string folder in Directory.GetDirectories(path))
            {
                loadedPlugins.AddRange(LoadPlugins(folder));
            }


            DirectoryCatalog catalog = new DirectoryCatalog(path, "*.dll");

            CompositionContainer container = new CompositionContainer(catalog);

            PluginWrapper wrapper = new PluginWrapper();

            container.SatisfyImportsOnce(wrapper);

            if (wrapper.LoadedPlugins != null)
            {
                loadedPlugins.AddRange(wrapper.LoadedPlugins);
            }

            return loadedPlugins;
        }
    }
}
