using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesting.Core.Classes.Classification;

namespace Nesting.Core.Interfaces
{
    public interface INestingClassifierFactory
    {
        ClassifierInformation ClassifierInformation { get;}

        INestingClassifier Create();
    }
}
