using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Interfaces;
using Nesting.Core.Interfaces;

namespace Nesting.SimpleTesselationClassifier
{
    [Export(typeof(IInterfacePlugin))]
    public class SimpleTesselationClassifierPlugin : IInterfacePlugin
    {
        public string PluginName
        {
            get { return "SimpleTesselationClassifier"; }
        }

        public void DeployPlugin(ICore applicationCore)
        {
            applicationCore.RegisterObject(new SimpleTesselationClassifierFactory(), true);
        }

        public void AllPluginsDeployed(ICore applicationCore)
        {
            //Make sure the classifiers are registered

            List<INestingClassifierFactory> factories = applicationCore.GetRegisteredObjects<INestingClassifierFactory>();

            SimpleTesselationClassifierFactory factory1 = factories.FirstOrDefault(x => x is SimpleTesselationClassifierFactory) as SimpleTesselationClassifierFactory;
            
            if (factory1 == null)
            {
                throw new Exception("Error! NestingClassifierFactories not found!");
            }
        }
    }
}
