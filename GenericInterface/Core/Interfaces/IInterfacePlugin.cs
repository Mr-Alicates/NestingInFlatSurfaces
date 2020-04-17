using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IInterfacePlugin
    {
        string PluginName { get; }

        //This method is invoked so every plugin registers their interfaces and objects in the core
        void DeployPlugin(ICore applicationCore);

        //This method is invoked after all plugins are registered, so they can read registered objects
        void AllPluginsDeployed(ICore applicationCore);
    }
}
