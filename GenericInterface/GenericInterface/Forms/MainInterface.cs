using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Interfaces;
using Core.Persistence;
using Core.Plugins;

[assembly: AssemblyVersion("0.0.2.0")]
[assembly: AssemblyFileVersion("0.0.2.0")]

namespace GenericInterface
{
    public partial class MainInterface : Form, ICore
    {

        private PluginsLoader loader = new PluginsLoader();

        public MainInterface()
        {
            InitializeComponent();
            
            loader.LoadPlugins(this, Environment.CurrentDirectory + ConfigurationManager.AppSettings["PluginsPath"]);

        }
        
        #region Implementation of ICore

        private IPersistenceService persistenceService;
        private List<object> registeredObjects = new List<object>();

        public IPersistenceService GetPersistenceService()
        {
            if (persistenceService == null)
            {
                persistenceService = new InMemoryPersistenceService();
            }

            return persistenceService;
        }

        public void AddToPluginMenu(string pluginName, ToolStripItem menu)
        {
            if (menu == null || string.IsNullOrEmpty(pluginName))
            {
                return;
            }

            ToolStripMenuItem pluginMenu = null;

            if (mnuStrip.Items.ContainsKey(pluginName))
            {
                pluginMenu = mnuStrip.Items[pluginName] as ToolStripMenuItem;

                if (pluginMenu == null)
                {
                    return;
                }
            }
            else
            {
                pluginMenu = new ToolStripMenuItem(pluginName);
                mnuStrip.Items.Add(pluginMenu);
            }
            

            //TODO: check null
            pluginMenu.DropDownItems.Add(menu);
        }

        public void AddToPluginMenu(string pluginName, Form form)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(pluginName);

            form.Dock = DockStyle.Fill;

            //This makes the form to open when the menu is clicked
            item.Click += (sender, args) => form.ShowDialog();

            item.Dock = DockStyle.Fill;

            AddToPluginMenu(pluginName, item);
        }

        public void AddToTabs(string pluginName, TabPage tab)
        {
            if (tab == null || string.IsNullOrEmpty(pluginName))
            {
                return;
            }

            tab.Name = pluginName;

            tabMain.TabPages.Add(tab);
        }

        public void AddToTabs(string pluginName, Form form)
        {
            //This allows forms to be designed as a regular window forms and be loaded as a tab into the interface.
            
            TabPage tab = new TabPage(pluginName);

            form.TopLevel = false;
            form.Parent = tab;
            form.Visible = true;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            //This event handler prevents forms embedded into the tab control from being closed (which would break the interface)
            form.Closing += (sender, args) =>
            {
                args.Cancel = true;
            };

            AddToTabs(pluginName, tab);
        }

        public void RegisterObject(object item, bool avoidDuplicates)
        {
            Type type = item.GetType();

            if (!avoidDuplicates || registeredObjects.All(x => x.GetType() != type))
            {
                registeredObjects.Add(item);
            }
        }

        public List<T> GetRegisteredObjects<T>()
        {
            //We select items of the selected type and then we cast them
            return registeredObjects.Where(x =>( x is T)).Select(x=>(T)x).ToList();
        }

        public List<IInterfacePlugin> GetLoadedPlugins()
        {
            return loader.LoadedPlugins;
        }

        #endregion ICore

    }
}
