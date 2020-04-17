using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Nesting.WorkingAreaEditor
{
    [Export(typeof(IInterfacePlugin))]
    public class WorkingAreaEditorPlugin : IInterfacePlugin
    {
        public string PluginName
        {
            get { return "WorkingAreaEditor"; }
        }

        public void DeployPlugin(ICore applicationCore)
        {
            applicationCore.AddToTabs(PluginName,new WorkingAreaEditor(applicationCore));
        }

        public void AllPluginsDeployed(ICore applicationCore)
        {
        }
    }
}
