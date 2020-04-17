using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes.Geometry;
using Core.Nesting;

namespace Nesting.Core.Classes.Nesting
{
    public class Part : Figure
    {
        public Part()
        {
            InnerParts = new List<Part>();
        }

        public List<Part> InnerParts { get; set; }

        public List<Point> InnerPartsVertexes
        {
            get
            {
                List<Point> results = Vertexes.Select(x => x.Clone()).ToList();
                results.AddRange(InnerParts.SelectMany(x => x.Vertexes).ToList());

                return results;
            }
        }

        public List<Point> ConvexHullVertexes
        {
            get
            {
                //Algorithm found in stackOverflow : http://stackoverflow.com/questions/10020949/gift-wrapping-algorithm

                List<Point> hull = new List<Point>();

                List<Point> points = InnerPartsVertexes;

                // get leftmost point
                Point vPointOnHull = points.Where(p => p.X == points.Min(min => min.X)).First();

                Point vEndpoint;
                do
                {
                    hull.Add(vPointOnHull);
                    vEndpoint = points[0];

                    for (int i = 1; i < points.Count; i++)
                    {
                        if ((vPointOnHull == vEndpoint)
                            || (Orientation(vPointOnHull, vEndpoint, points[i]) == -1))
                        {
                            vEndpoint = points[i];
                        }
                    }

                    vPointOnHull = vEndpoint;

                } while (vEndpoint != hull[0]);

                return hull;
            }

        }

        public IEnumerable<KeyValuePair<Point, Point>> ConvexHullEdges()
        {
            List<Point> convexHullVertexes = ConvexHullVertexes;

            for (int i = 0; i < convexHullVertexes.Count; i++)
            {
                yield return new KeyValuePair<Point, Point>(convexHullVertexes[i % convexHullVertexes.Count], convexHullVertexes[(i + 1) % convexHullVertexes.Count]);
            }
        }

        public Part Clone()
        {
            Part clone = new Part()
            {
                Description = this.Description,
                Id = this.Id,
                IsPlaced = this.IsPlaced,
                Name = this.Name,
                Vertexes = this.Vertexes.Select(v => v.Clone()).ToList(),
                InnerParts = this.InnerParts.Select(v => v.Clone()).ToList(),
            };

            if (this.Placement != null)
            {
                clone.Placement = this.Placement.Clone();
            }
        
            return clone;
        }
        
        private static int Orientation(Point p1, Point p2, Point p)
        {
            // Determinant
            float orin = (p2.X - p1.X) * (p.Y - p1.Y) - (p.X - p1.X) * (p2.Y - p1.Y);

            if (orin > 0)
                return -1; //          (* Orientation is to the left-hand side  *)
            if (orin < 0)
                return 1; // (* Orientation is to the right-hand side *)

            return 0; //  (* Orientation is neutral aka collinear  *)
        }
        
        public Part CalculateMirror(Point linePoint1, Point linePoint2)
        {
            Part mirror = this.Clone();
            mirror.InnerParts.Clear();

            foreach (Part innerPart in this.InnerParts)
            {
                Part innerMirror = innerPart.CalculateMirror(linePoint1, linePoint2);
                mirror.InnerParts.Add(innerMirror);
            }

            if (mirror.Vertexes.Any())
            {
                mirror.Vertexes = this.Vertexes.Select(x => x.CalculateMirror(linePoint1, linePoint2)).ToList();
            }

            return mirror;
        }

        public void FitToOrigin()
        {
            //Make sure that all points from all subparts / vertexes are inside the positive XY area
            Vector fitVector = CalculateFitVector(this.InnerParts.Any() ? InnerPartsVertexes : Vertexes);

            Fit(fitVector);
        }
        
        public void Fit(Vector fitVector)
        {
            if (this.InnerParts.Any())
            {
                InnerParts.ForEach(x => x.Fit(fitVector));
            }
            else
            {
                Vertexes.ForEach(x => x.Sum(fitVector));
            }
        }

        private static Vector CalculateFitVector(List<Point> vertexes)
        {
            float lowestX = vertexes.Select(x => x.X).Min();
            float lowestY = vertexes.Select(x => x.Y).Min();

            Point oldOrigin = new Point(lowestX, lowestY);

            return new Vector(oldOrigin, Point.Origin);
        }

        public override bool Equals(object obj)
        {
            Part other = obj as Part;

            if (other == null)
            {
                return false;
            }
            
            bool result = this.Vertexes.Count == other.Vertexes.Count;

            foreach (Point vertex in this.Vertexes)
            {
                result = result && other.Vertexes.Any(x => x.Equals(vertex));
            }

            result = result && this.InnerParts.Count == other.InnerParts.Count;

            foreach (Part part in this.InnerParts)
            {
                result = result && other.InnerParts.Any(x => x.Equals(part));
            }

            return result;
        }
    }
}
