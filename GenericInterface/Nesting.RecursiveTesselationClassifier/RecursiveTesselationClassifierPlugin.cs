using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Interfaces;
using Nesting.Core.Interfaces;

namespace Nesting.RecursiveTesselationClassifier
{
    [Export(typeof(IInterfacePlugin))]
    public class RecursiveTesselationClassivierPlugin : IInterfacePlugin
    {
        public string PluginName
        {
            get { return "RecursiveTesselationClassifier"; }
        }

        public void DeployPlugin(ICore applicationCore)
        {
            applicationCore.RegisterObject(new RecursiveTesselationClassifierFactory(), true);
        }

        public void AllPluginsDeployed(ICore applicationCore)
        {
            //Make sure the classifiers are registered

            List<INestingClassifierFactory> factories = applicationCore.GetRegisteredObjects<INestingClassifierFactory>();

            RecursiveTesselationClassifierFactory factory1 = factories.FirstOrDefault(x => x is RecursiveTesselationClassifierFactory) as RecursiveTesselationClassifierFactory;
            
            if (factory1 == null)
            {
                throw new Exception("Error! NestingClassifierFactories not found!");
            }
        }
    }
}
