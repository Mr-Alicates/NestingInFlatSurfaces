using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Nesting;
using Nesting.Core.Classes.Nesting;
using Nesting.Core.Interfaces;

namespace Nesting.Core.Classes
{
    public class NestingManager : INestingManager
    {
        #region Implementation of INestingManager

        public bool IsRectangle(WorkingArea area, out float width, out float height)
        {
            if (area.Vertexes.Count == 4)
            {
                //Might be a square(true), rectangle (true), parallelogram(FALSE)


                if ((Math.Abs(area.Vertexes[0].X - area.Vertexes[1].X) < 0.01f &&
                     Math.Abs(area.Vertexes[2].X - area.Vertexes[3].X) < 0.01f &&
                     Math.Abs(area.Vertexes[0].Y - area.Vertexes[3].Y) < 0.01f &&
                     Math.Abs(area.Vertexes[1].Y - area.Vertexes[2].Y) < 0.01f)
                    ||
                    (Math.Abs(area.Vertexes[0].Y - area.Vertexes[1].Y) < 0.01f &&
                     Math.Abs(area.Vertexes[2].Y - area.Vertexes[3].Y) < 0.01f &&
                     Math.Abs(area.Vertexes[0].X - area.Vertexes[3].X) < 0.01f &&
                     Math.Abs(area.Vertexes[1].X - area.Vertexes[2].X) < 0.01f))
                {

                    GetRectangle(area.Vertexes, out width, out height);
                    return true;
                }
            }

            width = -1;
            height = -1;
            return false;
        }

        public bool IsRightTriangle(Part part)
        {
            if (part.Vertexes.Count != 3)
            {
                return false;
            }

            //Use the pythagorean theoreme to check a^2 + b^2 = c^2
            List<float> sides = new List<float>()
            {
                new Vector(part.Vertexes[0],part.Vertexes[1]).GetModule(),
                new Vector(part.Vertexes[1],part.Vertexes[2]).GetModule(),
                new Vector(part.Vertexes[2],part.Vertexes[0]).GetModule(),
            };

            float hypotenuse = sides.Max();
            sides.Remove(hypotenuse);

            float sideOne = hypotenuse*hypotenuse;

            float sideTwo = sides[0]*sides[0] + sides[1]*sides[1];
            
            //We use a delta comparison to avoid comparison errors due to floating point rounding
            return Math.Abs(sideOne - sideTwo) < 0.001;
        }

        public void GetRectangleBoxOfPart(Part part, out float width, out float height)
        {
            GetRectangle(part.ConvexHullVertexes, out width, out height);
        }

        public void GetRectangleBoxOfParts(List<Part> parts, out float width, out float height)
        {
            //First we calculate the size of the box for each of the parts
            //we calculate boxes to fit the parts and try to find one that fits them all

            List<float> heights = new List<float>();
            List<float> widths = new List<float>();

            float boxX, boxY;
            foreach (var part in parts)
            {
                GetRectangleBoxOfPart(part, out boxX, out boxY);

                widths.Add(boxX);
                heights.Add(boxY);
            }

            width = widths.Max();
            height = heights.Max();
        }

        public Part CalculateFlippedRightTriangle(Part triangle)
        {
            if (!IsRightTriangle(triangle))
            {
                throw new InvalidOperationException("Part is not a triangle");
            }

            //A rectangle formed from a right triangle, shares 3 vertexes with it. 
            //I need to find the non shared one and the neighboring ones to build a flipped triangle.
            
            float boxX, boxY;
            GetRectangleBoxOfPart(triangle, out boxX, out boxY);

            Part flippedTriangle = triangle.Clone();
            flippedTriangle.Vertexes.Clear();

            List<Point> boxVertexes = new List<Point>()
            {
                new Point(0, 0),
                new Point(boxX, 0),
                new Point(boxX, boxY),
                new Point(0, boxY)
            };
            
            for (int i = 0; i < boxVertexes.Count; i++)
            {
                if (!triangle.Vertexes.Contains(boxVertexes[i]))
                {
                    flippedTriangle.Vertexes.Add(boxVertexes[(i + boxVertexes.Count - 1) % boxVertexes.Count]);
                    flippedTriangle.Vertexes.Add(boxVertexes[i]);
                    flippedTriangle.Vertexes.Add(boxVertexes[(i + 1) % boxVertexes.Count]);
                }
            }

            return flippedTriangle;
        }

        public Part CalculateRectangle(float width, float height, Point coordinates)
        {
            Part result = new Part()
            {
                Vertexes = new List<Point>()
                {
                    new Point(0, 0),
                    new Point(width, 0),
                    new Point(width, height),
                    new Point(0, height)
                }
            };

            result.Place(coordinates);

            return result;
        }

        public WorkingArea CalculateWorkingArea(float width, float height)
        {
            WorkingArea result = new WorkingArea()
            {
                Vertexes = new List<Point>()
                {
                    new Point(0, 0),
                    new Point(width, 0),
                    new Point(width, height),
                    new Point(0, height)
                },
            };

            return result;
        }

        #endregion


        private void GetRectangle(List<Point> vertexes, out float width, out float height)
        {
            List<float> xCoordinates = vertexes.Select(x => x.X).OrderBy(x => x).ToList();
            List<float> yCoordinates = vertexes.Select(x => x.Y).OrderBy(x => x).ToList();

            height = yCoordinates.Last() - yCoordinates.First();
            width = xCoordinates.Last() - xCoordinates.First();
        }
    }
}
