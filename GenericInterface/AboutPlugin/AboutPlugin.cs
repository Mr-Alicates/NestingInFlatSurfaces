using System.ComponentModel.Composition;
using Core.Interfaces;
using GenericInterface.Forms;

namespace AboutPlugin
{
    [Export(typeof(IInterfacePlugin))]
    public class AboutPlugin : IInterfacePlugin
    {
        public string PluginName
        {
            get { return "AboutPlugin"; }
        }

        public void DeployPlugin(ICore applicationCore)
        {
            applicationCore.AddToPluginMenu(PluginName,new About());
        }

        public void AllPluginsDeployed(ICore applicationCore)
        {
        }
    }
}
