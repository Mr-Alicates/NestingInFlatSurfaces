using Nesting.Core.Classes.Classification;
using Nesting.Core.Interfaces;

namespace Nesting.DemoClassifiers
{
    public class VeryVerySlowClassifierFactory : INestingClassifierFactory
    {
        public ClassifierInformation ClassifierInformation
        {
            get { return VeryVerySlowClassifier.ClassifierInformation; }
        }

        public INestingClassifier Create()
        {
            return new VeryVerySlowClassifier();
        }
    }
}
