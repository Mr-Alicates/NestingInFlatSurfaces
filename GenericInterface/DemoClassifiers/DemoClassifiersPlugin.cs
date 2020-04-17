using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Interfaces;
using Nesting.Core.Interfaces;

namespace Nesting.DemoClassifiers
{
    [Export(typeof(IInterfacePlugin))]
    public class PartEditorPlugin : IInterfacePlugin
    {
        public string PluginName
        {
            get { return "DemoClassifiers"; }
        }

        public void DeployPlugin(ICore applicationCore)
        {
            applicationCore.RegisterObject(new VeryVerySlowClassifierFactory(), true);
            applicationCore.RegisterObject(new DemoClassifierFactory(), true);
        }

        public void AllPluginsDeployed(ICore applicationCore)
        {
            //Make sure the classifiers are registered

            List<INestingClassifierFactory> factories = applicationCore.GetRegisteredObjects<INestingClassifierFactory>();

            VeryVerySlowClassifierFactory factory1 = factories.FirstOrDefault(x => x is VeryVerySlowClassifierFactory) as VeryVerySlowClassifierFactory;

            DemoClassifierFactory factory2 = factories.FirstOrDefault(x => x is DemoClassifierFactory) as DemoClassifierFactory;

            if (factory1 == null || factory2 == null)
            {
                throw new Exception("Error! NestingClassifierFactories not found!");
            }
        }
    }
}
