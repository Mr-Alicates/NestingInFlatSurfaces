using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.Interfaces
{
    public interface ICore
    {
        IPersistenceService GetPersistenceService();

        void AddToPluginMenu(string pluginName, ToolStripItem menu);

        void AddToPluginMenu(string pluginName, Form form);

        void AddToTabs(string pluginName, TabPage tab);

        void AddToTabs(string pluginName, Form form);

        void RegisterObject(object item, bool avoidDuplicates);

        List<T> GetRegisteredObjects<T>();

        List<IInterfacePlugin> GetLoadedPlugins();
    }
}
