using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Nesting.Core.Interfaces;

namespace Nesting.Runner
{
    [Export(typeof(IInterfacePlugin))]
    public class NestingRunnerPlugin : IInterfacePlugin
    {
        private NestingRunner form;

        public string PluginName
        {
            get { return "NestingRunner"; }
        }

        public void DeployPlugin(ICore applicationCore)
        {
            form = new NestingRunner(applicationCore);

            applicationCore.AddToTabs(PluginName, form);
        }

        public void AllPluginsDeployed(ICore applicationCore)
        {
            List<INestingClassifierFactory> factories = applicationCore.GetRegisteredObjects<INestingClassifierFactory>();

            form.LoadClassifierFactories(factories);
        }
    }
}
