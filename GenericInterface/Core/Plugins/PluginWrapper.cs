using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Plugins
{
    public class PluginWrapper
    {
        [ImportMany]
        public List<IInterfacePlugin> LoadedPlugins;
    }
}
