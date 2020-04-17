using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Nesting.Core.Interfaces;

namespace Nesting.ClassifierList
{
    [Export(typeof(IInterfacePlugin))]
    public class ClassifierListPlugin : IInterfacePlugin
    {
        private ClassifierList form = new ClassifierList();

        public string PluginName
        {
            get { return "ClassifierList"; }
        }

        public void DeployPlugin(ICore applicationCore)
        {
            applicationCore.AddToTabs(PluginName,form);
        }

        public void AllPluginsDeployed(ICore applicationCore)
        {
            List<INestingClassifierFactory> factories = applicationCore.GetRegisteredObjects<INestingClassifierFactory>();

            form.LoadClassifierFactories(factories);
        }
    }
}
