using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Core.Nesting;
using Nesting.Core.Classes;
using Nesting.Core.Classes.Classification;
using Nesting.Core.Classes.Nesting;
using Nesting.Core.Interfaces;

namespace Nesting.DemoClassifiers
{
    public class VeryVerySlowClassifier : INestingClassifier
    {
        public static ClassifierInformation ClassifierInformation = new ClassifierInformation("VeryVerySlow classifier", "0.0.1",
    "This classifier is for testing pourposes only. It is very slow. It offers a single wrong result.", Assembly.GetAssembly(typeof(VeryVerySlowClassifier)).FullName);

        private ClassificationParameters parameters = null;

        #region Implementation of INestingClassifier

        public void SetNestingManager(INestingManager nestingManager)
        {
            //This classifier does not use Nesting Manager
        }
        
        public void SetClassificationParameters(ClassificationParameters newParameters)
        {
            this.parameters = newParameters;
        }
        
        public ClassificationResult ClassifyOne()
        {
            //This classifier does not classify

            ClassificationResult result = new ClassificationResult(ClassifierInformation);

            result.WorkingArea = (WorkingArea)parameters.WorkingArea.Clone();

            List<Part> parts = new List<Part>();
            foreach (Part part in parameters.Parts)
            {
                Part classifiedPart = (Part)part.Clone();
                classifiedPart.Place(new Point(5, 5));
                parts.Add(classifiedPart);
            }

            result.Parts = parts;

            Thread.Sleep(15000);

            return result;
        }

        public List<ClassificationResult> ClassifyAll()
        {
            return new List<ClassificationResult>() { ClassifyOne() };
        }

        public ClassifierInformation GetClassifierInformation()
        {
            return ClassifierInformation;
        }

        #endregion
    }
}
