using Nesting.Core.Classes.Classification;
using Nesting.Core.Interfaces;

namespace Nesting.DemoClassifiers
{
    public class DemoClassifierFactory : INestingClassifierFactory
    {
        public ClassifierInformation ClassifierInformation
        {
            get
            {
                return DemoClassifier.ClassifierInformation;
            }
        }

        public INestingClassifier Create()
        {
            return new DemoClassifier();
        }
    }
}
