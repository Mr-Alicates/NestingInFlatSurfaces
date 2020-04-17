using Nesting.Core.Classes.Classification;
using Nesting.Core.Interfaces;

namespace Nesting.RecursiveTesselationClassifier
{
    public class RecursiveTesselationClassifierFactory :INestingClassifierFactory
    {
        public ClassifierInformation ClassifierInformation
        {
            get
            {
                return RecursiveTesselationClassifier.ClassifierInformation;
            }
        }

        public INestingClassifier Create()
        {
            return new RecursiveTesselationClassifier();
        }
    }
}
