using Nesting.Core.Classes.Classification;
using Nesting.Core.Interfaces;

namespace Nesting.DemoClassifiers
{
    public class ConvexHullSymmetryClassifierFactory : INestingClassifierFactory
    {
        public ClassifierInformation ClassifierInformation
        {
            get
            {
                return ConvexHullSimmetryClassifier.ClassifierInformation;
            }
        }

        public INestingClassifier Create()
        {
            return new ConvexHullSimmetryClassifier();
        }
    }
}
