using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Interfaces;
using Nesting.Core.Interfaces;

namespace Nesting.DemoClassifiers
{
    [Export(typeof(IInterfacePlugin))]
    public class ConvexHullSimmetryClassifierPlugin : IInterfacePlugin
    {
        public string PluginName
        {
            get { return "ConvexHullSimmetryClassifier"; }
        }

        public void DeployPlugin(ICore applicationCore)
        {
            applicationCore.RegisterObject(new ConvexHullSymmetryClassifierFactory(), true);
        }

        public void AllPluginsDeployed(ICore applicationCore)
        {
            //Make sure the classifiers are registered
            
            ConvexHullSymmetryClassifierFactory factory = applicationCore.GetRegisteredObjects<INestingClassifierFactory>().FirstOrDefault(x => x is ConvexHullSymmetryClassifierFactory) as ConvexHullSymmetryClassifierFactory;

            if (factory == null)
            {
                throw new Exception("Error! ConvexHullSymmetryClassifierFactory not found!");
            }
        }
    }
}
