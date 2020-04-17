using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Nesting
{
    public class Point
    {
        public static Point Origin { get { return new Point(0,0);} }

        public static bool AreCollinear(Point pointOne, Point pointTwo, Point pointThree)
        {
            Vector one = new Vector(pointOne, pointTwo);
            Vector two = new Vector(pointOne, pointThree);
            Vector three = new Vector(pointTwo, pointThree);

            if (Math.Abs(one.Slope() - two.Slope()) > 0.001)
            {
                return false;
            }

            if (Math.Abs(two.Slope() - three.Slope()) > 0.001)
            {
                return false;
            }

            return true;
        }

        public Point CalculateMirror(Point line1, Point line2)
        {
            Point toProject = this.Clone();

            if (Point.AreCollinear(toProject, line1, line2))
            {
                return toProject;
            }

            float  x, y;

            float m = (line2.Y - line1.Y) / (line2.X - line1.X);

            if (float.IsInfinity(m))
            {
                x = line1.X;
                y = toProject.Y;
            }
            else
            {
                float b = line1.Y - (m*line1.X);
                x = (m*toProject.Y + toProject.X - m*b)/(m*m + 1);
                y = (m*m*toProject.Y + m*toProject.X + b)/(m*m + 1);
            }

            Point projection = new Point(x, y);

            Vector vector = new Vector(this.Clone(), projection);

            toProject.Sum(vector);
            toProject.Sum(vector);

            return toProject;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public Point()
        {

        }

        public Point(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Multiply(float value)
        {
            this.X = this.X*value;
            this.Y = this.Y*value;
        }

        public void Sum(Vector vector)
        {
            float sumX = vector.X;
            float sumY = vector.Y;

            X = X + sumX;
            Y = Y + sumY;
        }

        public override string ToString()
        {
            string result = "";

            result = "[" + X + "," + Y + "]";

            return result;
        }

        public static Point operator +(Point one, Point two)
        {
            if (one == null)
            {
                one = Point.Origin;
            }

            if (two == null)
            {
                two = Point.Origin;
            }

            Point result = new Point(one.X + two.X, one.Y + two.Y);

            return result;
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }

        public PointF ToPointF()
        {
            return new PointF(X, Y);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        protected bool Equals(Point other)
        {
            if (other == null)
            {
                return false;
            }

            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
    }
}
