using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Nesting;
using Nesting.Core.Classes;
using Nesting.Core.Classes.Classification;
using Nesting.Core.Classes.Nesting;
using Nesting.Core.Interfaces;

namespace Nesting.SimpleTesselationClassifier
{
    public class SimpleTesselationClassifier : INestingClassifier
    {
        public static ClassifierInformation ClassifierInformation = new ClassifierInformation("SimpleTesselationClassifier", "1.0.0",
            "This classifier forms a rectangle covering every piece and repeats it all over the working area. Returns a result for every part given.", Assembly.GetAssembly(typeof(SimpleTesselationClassifier)).FullName);

        private ClassificationParameters classificationParameters;
        private INestingManager manager;
        
        #region Implementation of INestingClassifier

        public void SetNestingManager(INestingManager nestingManager)
        {
            this.manager = nestingManager;
        }

        public void SetClassificationParameters(ClassificationParameters parameters)
        {
            this.classificationParameters = parameters;
        }

        public List<ClassificationResult> ClassifyAll()
        {
            List<ClassificationResult> results = new List<ClassificationResult>();

            foreach (Part part in classificationParameters.Parts)
            {
                results.Add(ClassifyGeneric(manager, new List<Part>() { part }, classificationParameters.WorkingArea));
            }

            results.Add(ClassifyGeneric(manager, classificationParameters.Parts,classificationParameters.WorkingArea));

            return results;
        }

        public ClassifierInformation GetClassifierInformation()
        {
            return ClassifierInformation;
        }

        #endregion

        private static ClassificationResult ClassifyGeneric(
            INestingManager manager,
            List<Part> parts,
            WorkingArea workingArea)
        {
            float areaX, areaY;

            if (!manager.IsRectangle(workingArea, out areaX, out areaY))
            {
                throw new Exception("WorkingArea is not a rectangle!");
            }
            
            float boxX, boxY;
            manager.GetRectangleBoxOfParts(parts, out boxX, out boxY);

            if (boxX > areaX || boxY > areaY)
            {
                throw new Exception("Part does not fit inside Working area!");
            }
            
            //Calculate how many boxes could be fit in the area
            int horizontalBoxes = (int)(areaX / boxX);
            int verticalBoxes = (int)(areaY / boxY);
            
            //We build the final result using the partial results and the calculated positions of the boxes
            ClassificationResult result = new ClassificationResult(ClassifierInformation)
            {
                WorkingArea = workingArea.Clone()
            };
            
            int partIndex = 0;
            
            //Place parts until we run out of space
            for (int i = 0; i < horizontalBoxes; i++)
            {
                for (int j = 0; j < verticalBoxes; j++)
                {
                    Point subResultOrigin = new Point(i * boxX, j * boxY);
                    result.ExtraPolygons.Add(manager.CalculateRectangle(boxX, boxY, subResultOrigin));

                    Part placedPart = parts[partIndex % parts.Count].Clone();
                    
                    placedPart.Place(subResultOrigin);

                    result.Parts.Add(placedPart);

                    partIndex++;
                }
            }

            return result;
        }       
    }
}
