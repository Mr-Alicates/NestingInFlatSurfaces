using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesting.Core.Classes;
using Nesting.Core.Classes.Classification;

namespace Nesting.Core.Interfaces
{
    public interface INestingClassifier
    {
        void SetNestingManager(INestingManager nestingManager);

        void SetClassificationParameters(ClassificationParameters parameters);

        List<ClassificationResult> ClassifyAll();

        ClassifierInformation GetClassifierInformation();
    }
}
