using Nesting.Core.Classes.Classification;
using Nesting.Core.Interfaces;

namespace Nesting.SimpleTesselationClassifier
{
    public class SimpleTesselationClassifierFactory :INestingClassifierFactory
    {
        public ClassifierInformation ClassifierInformation
        {
            get
            {
                return SimpleTesselationClassifier.ClassifierInformation;
            }
        }

        public INestingClassifier Create()
        {
            return new SimpleTesselationClassifier();
        }
    }
}
