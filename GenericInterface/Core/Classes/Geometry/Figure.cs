using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Nesting;

namespace Core.Classes.Geometry
{
    public abstract class Figure : IPersistible
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Point> Vertexes { get; set; }

        public bool IsPlaced { get; set; }
        public Point Placement { get; set; }
        
        public Figure()
        {
            Vertexes = new List<Point>();
            Placement = Point.Origin;
        }

        public void Place(Point point)
        {
            Placement = point;
            IsPlaced = true;
        }

        public void UnPlace()
        {
            Placement = null;
            IsPlaced = false;
        }
        
        public float GetTotalArea()
        {
            //Algorighm found in StackOverflow: http://stackoverflow.com/questions/2432428/is-there-any-algorithm-for-calculating-area-of-a-shape-given-co-ordinates-that-d

            int i, j;
            double area = 0;

            for (i = 0; i < Vertexes.Count; i++)
            {
                j = (i + 1) % Vertexes.Count;

                area += Vertexes[i].X * Vertexes[j].Y;
                area -= Vertexes[i].Y * Vertexes[j].X;
            }

            area /= 2;
            return (float) (area < 0 ? -area : area);
        }

        public void NormalizeVertexes()
        {
            if (Vertexes.Count > 0)
            {
                float minimumY = Vertexes.Select(vertex => vertex.Y).Min();
                float minimumX = Vertexes.Select(vertex => vertex.X).Min();

                foreach (Point point in Vertexes)
                {
                    point.Y = point.Y - minimumY;
                    point.X = point.X - minimumX;
                }
            }
        }
    }
}
