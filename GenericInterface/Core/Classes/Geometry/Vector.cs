using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Nesting
{
    public class Vector
    {
        public Point Beginning { get; set; }
        public Point End { get; set; }

        public float X { get { return End.X - Beginning.X; } }
        public float Y { get { return End.Y - Beginning.Y; } }

        public Vector()
        {
            this.Beginning = new Point();
            this.End = new Point();

        }

        public Vector(Point beggining, Point end)
        {
            this.Beginning = beggining;
            this.End = end;
        }

        public Vector(Point end)
        {
            this.End = end;
            this.Beginning = new Point(0, 0);
        }

        public Vector(float x, float y)
        {
            this.End = new Point(x, y);
            this.Beginning = new Point(0, 0);
        }

        public float GetModule()
        {
            float res = X * X + Y * Y;

            res = (float)Math.Sqrt(res);

            return res;
        }

        public float Slope()
        {
            return X/Y;
        }

        public Vector PerpendicularClockwise()
        {
            return new Vector(-Y, X);
        }

        public  Vector PerpendicularCounterClockwise()
        {
            return new Vector(Y, -X);
        }

        public Vector GetUnitary()
        {
            float module = this.GetModule();

            Vector result = new Vector(this.X / module, this.Y / module);

            return result;
        }

        public float GetAngle(Vector vector)
        {
            return (float)Math.Atan((this.Y - vector.Y) / (vector.X - this.X));
        }
        
        public Vector Clone()
        {
            return new Vector(this.Beginning.Clone(), this.End.Clone());
        }

    }
}
