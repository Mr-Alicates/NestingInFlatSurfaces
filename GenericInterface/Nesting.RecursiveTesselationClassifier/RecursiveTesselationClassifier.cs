using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Nesting;
using Nesting.Core.Classes;
using Nesting.Core.Classes.Classification;
using Nesting.Core.Classes.Nesting;
using Nesting.Core.Interfaces;

namespace Nesting.RecursiveTesselationClassifier
{
    public class RecursiveTesselationClassifier : INestingClassifier
    {
        public static ClassifierInformation ClassifierInformation = new ClassifierInformation(
            "RecursiveTesselationClassifier", 
            "1.0.0",
            "This classifier uses tesselation to cover the main parts of the work area, then repeating the execution in the remaining areas.", 
            Assembly.GetAssembly(typeof(RecursiveTesselationClassifier)).FullName);

        private ClassificationParameters classificationParameters;
        private INestingManager manager;

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
            ClassificationResult mainResult = GetMainResult(
                manager, 
                classificationParameters.Parts,
                classificationParameters.WorkingArea);

            if (mainResult == null)
            {
                return new List<ClassificationResult>();
            }

            return new List<ClassificationResult>() { mainResult };
        }

        public ClassifierInformation GetClassifierInformation()
        {
            return ClassifierInformation;
        }

        #region General Algorithm

        public static ClassificationResult GetMainResult(
            INestingManager nestingManager, 
            List<Part> originalParts,
            WorkingArea workingArea)
        {
            float areaX, areaY;
            if (!nestingManager.IsRectangle(workingArea, out areaX, out areaY))
            {
                throw new Exception("WorkingArea is not a rectangle!");
            }

            //Step 1: Remove parts that do not fit the working area
            float boxX;
            float boxY;
            List<Part> parts = new List<Part>();

            foreach (Part part in originalParts)
            {
                nestingManager.GetRectangleBoxOfPart(part, out boxX, out boxY);

                if (areaX >= boxX && areaY >= boxY)
                {
                    //Part does not fit the working area
                    parts.Add(part);
                }
            }

            //Step 2: Break if no parts fit the working area
            if (!parts.Any())
            {
                //Empty result
                return new ClassificationResult(ClassifierInformation)
                {
                    WorkingArea = workingArea
                };
            }

            //Step 2.5: Sort parts by Area size in descending order, so we favor the placement of bigger pieces 
            //in case there are not enough polygons to place all the parts, because the smaller parts could be fit in the remainders
            parts = parts.OrderByDescending(x => x.GetTotalArea()).ToList();

            //Step 3: Calculate the tesselation polygon
            nestingManager.GetRectangleBoxOfParts(parts, out boxX, out boxY);
            int horizontalBoxes = (int)(areaX / boxX);
            int verticalBoxes = (int)(areaY / boxY);

            WorkingArea tesselationPolygon = new WorkingArea()
            {
                Vertexes = new List<Point>()
                {
                    new Point(0, 0),
                    new Point(boxX, 0),
                    new Point(boxX, boxY),
                    new Point(0, boxY)
                }
            };

            ClassificationResult result = new ClassificationResult(ClassifierInformation);

            //Step 4: Calculate the remainder polygons
            List<WorkingArea> remainderPolygons = CalculateRemainderWorkingAreas(
                nestingManager,
                boxX,
                boxY,
                horizontalBoxes,
                verticalBoxes,
                areaX,
                areaY);

            //Step 5: Generate the reimainder polygons' problems
            foreach (WorkingArea remainderPolygon in remainderPolygons)
            {
                ClassificationResult subResult = GetMainResult(nestingManager, parts, remainderPolygon);
                
                if (!nestingManager.IsRectangle(remainderPolygon, out areaX, out areaY))
                {
                    throw new Exception("WorkingArea is not a rectangle!");
                }

                result.ExtraPolygons.Add(nestingManager.CalculateRectangle(areaX, areaY, remainderPolygon.Placement));
                
                result.AddSubResult(subResult, remainderPolygon.Placement);
            }

            //Step6: Generate tesselation poligons' subproblem
            List<ClassificationResult> subresults = new List<ClassificationResult>();

            foreach (Part part in parts)
            {
                ClassificationResult res = GetDirectedResult(nestingManager, part, tesselationPolygon, parts);
                if (res != null)
                {
                    subresults.Add(res);
                }
            }
            
            result.WorkingArea = workingArea.Clone();

            int numberOfParts = parts.Count;
            int partIndex = 0;

            //Place parts until we run out of space
            for (int i = 0; i < horizontalBoxes; i++)
            {
                for (int j = 0; j < verticalBoxes; j++)
                {
                    ClassificationResult subResult = subresults[partIndex % numberOfParts];
                    Point subResultOrigin = new Point(i * boxX, j * boxY);

                    result.AddSubResult(subResult, subResultOrigin);

                    partIndex++;
                }
            }

            return result;
        }

        private static List<WorkingArea> CalculateRemainderWorkingAreas(
            INestingManager nestingManager,
            float tesselationPolygonWidth,
            float tesselationPolygonHeight,
            int horizontalBoxes, 
            int verticalBoxes,
            float workingAreaWidth,
            float workingAreaHeight)
        {
            List<WorkingArea> result = new List<WorkingArea>();

            float width1 = tesselationPolygonWidth * horizontalBoxes;
            float height1 = workingAreaHeight - tesselationPolygonHeight*verticalBoxes;

            float width2 = workingAreaWidth - tesselationPolygonWidth * horizontalBoxes;
            float height2 = height1;

            float width3 = width2;
            float height3 = tesselationPolygonHeight * verticalBoxes;

            if (height1 > 0)
            {
                WorkingArea area = nestingManager.CalculateWorkingArea(width1, height1);
                area.Placement = new Point(0,tesselationPolygonHeight * verticalBoxes);
                result.Add(area);
            }

            if (height2 > 0 && width2 > 0)
            {

                WorkingArea area = nestingManager.CalculateWorkingArea(width2, height2);
                area.Placement = new Point(tesselationPolygonWidth * horizontalBoxes, tesselationPolygonHeight * verticalBoxes);
                result.Add(area);
            }

            if (width3 > 0)
            {

                WorkingArea area = nestingManager.CalculateWorkingArea(width3, height3);
                area.Placement = new Point(tesselationPolygonWidth * horizontalBoxes, 0);
                result.Add(area);
            }

            return result;
        }

        #endregion

        #region Directed Algorithm
        
        private static ClassificationResult GetDirectedResult(INestingManager nestingManager, Part part, WorkingArea workingArea, List<Part> originalParts)
        {
            float areaX, areaY;
            if (!nestingManager.IsRectangle(workingArea, out areaX, out areaY))
            {
                throw new Exception("WorkingArea is not a rectangle!");
            }


            //Step 1: Calculate the tesselation polygon
            float boxX, boxY;
            nestingManager.GetRectangleBoxOfPart(part, out boxX, out boxY);
            int horizontalBoxes = (int)(areaX / boxX);
            int verticalBoxes = (int)(areaY / boxY);

            ClassificationResult result = new ClassificationResult(ClassifierInformation);

            //Step 4: Calculate the remainder polygons
            List<WorkingArea> remainderPolygons = CalculateRemainderWorkingAreas(
                nestingManager,
                boxX,
                boxY,
                horizontalBoxes,
                verticalBoxes,
                areaX,
                areaY);

            //Step 5: Generate the reimainder polygons' problems
            foreach (WorkingArea remainderPolygon in remainderPolygons)
            {
                ClassificationResult subResult = GetMainResult(nestingManager, originalParts, remainderPolygon);

                if (!nestingManager.IsRectangle(remainderPolygon, out areaX, out areaY))
                {
                    throw new Exception("WorkingArea is not a rectangle!");
                }

                result.ExtraPolygons.Add(nestingManager.CalculateRectangle(areaX, areaY, remainderPolygon.Placement));

                result.AddSubResult(subResult, remainderPolygon.Placement);
            }

            for (int i = 0; i < horizontalBoxes; i++)
            {
                for (int j = 0; j < verticalBoxes; j++)
                {
                    Point placementPart = new Point(i * boxX, j * boxY);

                    PlaceGenericPart(nestingManager, placementPart, part, result);

                    result.ExtraPolygons.Add(nestingManager.CalculateRectangle(boxX, boxY, placementPart));
                }
            }

            return result;
        }

        private static void PlaceGenericPart(INestingManager nestingManager, Point originOfTesselationPolygon, Part part, ClassificationResult result)
        {
            part.Place(Point.Origin);

            if (nestingManager.IsRightTriangle(part))
            {
                Part flippedTriangle = nestingManager.CalculateFlippedRightTriangle(part);
                flippedTriangle.Place(Point.Origin);

                PlacePart(originOfTesselationPolygon, flippedTriangle, result);
            }

            PlacePart(originOfTesselationPolygon, part, result);
        }

        private static void PlacePart(Point originOfTesselationPolygon, Part part, ClassificationResult result)
        {
            Part placedPart = part.Clone();

            placedPart.Place(originOfTesselationPolygon);

            result.Parts.Add(placedPart);
        }
        
        #endregion
    }
}
