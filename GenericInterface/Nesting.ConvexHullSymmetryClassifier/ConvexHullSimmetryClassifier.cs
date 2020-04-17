using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Nesting;
using Nesting.Core.Classes;
using Nesting.Core.Classes.Classification;
using Nesting.Core.Classes.Nesting;
using Nesting.Core.Interfaces;

namespace Nesting.DemoClassifiers
{
    public class ConvexHullSimmetryClassifier : INestingClassifier
    {
        public static ClassifierInformation ClassifierInformation = new ClassifierInformation("ConvexHullSimmetryClassifier classifier", "1.0.0",
    "This classifier uses a convex hull and symmetry approach", Assembly.GetAssembly(typeof(ConvexHullSimmetryClassifier)).FullName);

        private ClassificationParameters parameters = null;
        private INestingManager _nestingManager;

        #region Implementation of INestingClassifier

        public void SetNestingManager(INestingManager nestingManager)
        {
            if (nestingManager == null)
            {
                throw new ArgumentNullException(nameof(nestingManager));
            }

            _nestingManager = nestingManager;
        }

        public void SetClassificationParameters(ClassificationParameters newParameters)
        {
            this.parameters = newParameters;
        }
        
        public List<ClassificationResult> ClassifyAll()
        {
            List<ClassificationResult> results = new List<ClassificationResult>();

            foreach (Part part in parameters.Parts)
            {
                ClassificationResult result = new ClassificationResult(ClassifierInformation)
                {
                    Parts = new List<Part>() { part },
                    WorkingArea = parameters.WorkingArea
                };

                results.Add(result);

                ClassifySinglePart(_nestingManager, part, parameters.WorkingArea,results);
            }

            return results;
        }

        private static void ClassifySinglePart(INestingManager nestingManager, Part part, WorkingArea workingArea, List<ClassificationResult> results)
        {
            Point point = Point.Origin;
            part.Place(Point.Origin);


            float areaWidth, areaHeight;

            if (!nestingManager.IsRectangle(workingArea, out areaWidth, out areaHeight))
            {
                throw new InvalidOperationException("Area is not a rectangle");
            }
            
            foreach (KeyValuePair<Point, Point> edge in part.ConvexHullEdges())
            {
                Part mirror = part.CalculateMirror(edge.Key, edge.Value);
                Part clone = part.Clone();

                mirror.Place(point);
                clone.Place(point);

                Part hull = new Part();

                foreach (var side in new Part[] {mirror, clone})
                {
                    if (side.InnerParts.Any())
                    {
                        hull.InnerParts.AddRange(side.InnerParts.Select(x => x.Clone()));
                    }
                    else
                    {
                        hull.InnerParts.Add(side);
                    }
                }
                
                hull.FitToOrigin();

                float hullWidth, hullHeight;

                nestingManager.GetRectangleBoxOfPart(hull, out hullWidth, out hullHeight);

                //Is the hull too big for the area?
                if (!(hullWidth <= areaWidth) || !(hullHeight <= areaHeight))
                {
                    continue;
                }

                ClassificationResult result = new ClassificationResult(ClassifierInformation)
                {
                    Parts = hull.InnerParts,
                    WorkingArea = workingArea
                };

                //The result was already found?
                if (results.Contains(result))
                {
                    //Ignore result
                    continue;
                }

                //If it fits, we store the part and continue operating over it.
                results.Add(result);

                ClassifySinglePart(nestingManager, hull, workingArea, results);
            }
        } 

        public ClassifierInformation GetClassifierInformation()
        {
            return ClassifierInformation;
        }

        #endregion
    }
}
