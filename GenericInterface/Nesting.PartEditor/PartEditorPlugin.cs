using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Nesting.PartEditor
{
    [Export(typeof(IInterfacePlugin))]
    public class PartEditorPlugin : IInterfacePlugin
    {
        public string PluginName
        {
            get { return "PartEditor"; }
        }

        public void DeployPlugin(ICore applicationCore)
        {
            applicationCore.AddToTabs(PluginName,new PartEditor(applicationCore));
        }

        public void AllPluginsDeployed(ICore applicationCore)
        {
        }
    }
}
