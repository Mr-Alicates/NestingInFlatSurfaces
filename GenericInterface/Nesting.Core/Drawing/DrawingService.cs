using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Classes.Geometry;
using Core.Nesting;
using Nesting.Core.Classes.Classification;
using Nesting.Core.Classes.Nesting;
using Point = Core.Nesting.Point ;

namespace Nesting.Core.Drawing
{
    public class DrawingService
    {
        public Pen BorderPen;
        public Pen VertexPen;
        public Pen EdgePen;
        public Pen ExtraPolygonPen;
        public Pen ConvexHullPen;

        public Color FillColor;
        public Color TextColor;
        public Color FigureFillColorBack;
        public Color FigureFillColorFront;
        public Color ConvexHullFillColorBack;
        public Color ConvexHullFillColorFront;

        public Font Font;

        public DrawingService()
        {
            BorderPen = new Pen(Color.Black, 1);
            VertexPen = new Pen(Color.Blue, 2);
            EdgePen = new Pen(Color.Red, 1);
            ExtraPolygonPen = new Pen(Color.Purple, 1);
            ConvexHullPen = new Pen(Color.Crimson, 1);

            FillColor = Color.LightGray;
            TextColor = Color.Black;
            FigureFillColorBack = Color.DimGray;
            FigureFillColorFront = Color.LightGray;

            ConvexHullFillColorBack = Color.DarkRed;
            ConvexHullFillColorFront = Color.PaleVioletRed;

            Font = new Font("Arial", 11.0f);
        }

        #region VertexHelperMethods

        private static int GetSize(List<Point> vertexes)
        {
            List<float> coordinates = vertexes.Select(vertex => vertex.Y).ToList();

            coordinates.AddRange(vertexes.Select(vertex => vertex.X));

            return (int)coordinates.Max() + 1;
        }
        
        private static List<Point> NormalizeVertexes(List<Point> sourceVertexes)
        {
            //First, I must normalize the part coordinates

            float minimumY = sourceVertexes.Select(vertex => vertex.Y).Min();
            float minimumX = sourceVertexes.Select(vertex => vertex.X).Min();

            foreach (Point point in sourceVertexes)
            {
                point.Y = point.Y - minimumY;
                point.X = point.X - minimumX;
            }

            return sourceVertexes;
        }

        private static List<Point> ScaleVertexes(List<Point> normalizedVertexes, float scaleFactor)
        {
            //Vertexes must be scaled to fit into target image size

            foreach (Point point in normalizedVertexes)
            {
                point.Multiply(scaleFactor);
            }

            return normalizedVertexes;
        }

        private static List<Point> CenterVertexes(List<Point> scaledVertexes, int targetSize)
        {
            //Vertexes should be centered into target image

            float maximumY = scaledVertexes.Select(vertex => vertex.Y).Max();
            float maximumX = scaledVertexes.Select(vertex => vertex.X).Max();

            float marginY = (targetSize - 1 - maximumY) / 2;
            float marginX = (targetSize - 1 - maximumX) / 2;

            foreach (Point point in scaledVertexes)
            {
                point.Y = point.Y + marginY;
                point.X = point.X + marginX;
            }

            return scaledVertexes;
        } 
        
        #endregion

        #region DrawingMethods

        private static Bitmap DrawVertexes(Bitmap bitmap, List<Point> vertexes, Pen pen)
        {
            Graphics graphics = Graphics.FromImage(bitmap);

            for (int i = 0; i < vertexes.Count; i++)
            {
                float x = vertexes[i].X;
                float y = vertexes[i].Y;


                graphics.DrawEllipse(pen, x - 2, y - 2, 4, 4);
            }

            return bitmap;
        }

        private static Bitmap DrawPolygon(Bitmap bitmap, List<Point> vertexes, Pen pen, Point vector = null)
        {
            if (vertexes.Count == 0)
            {
                return bitmap;
            }

            Graphics graphics = Graphics.FromImage(bitmap);

            float vectorX = 0;
            float vectorY = 0;

            if (vector != null)
            {
                vectorX = vector.X;
                vectorY = vector.Y;
            }

            for (int i = 0; i < vertexes.Count - 1; i++)
            {
                graphics.DrawLine(pen, vertexes[i].X + vectorX, vertexes[i].Y + vectorY, vertexes[i + 1].X + vectorX, vertexes[i + 1].Y + vectorY);
            }

            graphics.DrawLine(pen, vertexes.Last().X + vectorX, vertexes.Last().Y + vectorY, vertexes.First().X + vectorX, vertexes.First().Y + vectorY);

            return bitmap;
        }

        public Bitmap GenerateBitmapWithBorder(int bitmapSize, Pen pen, Color fillColor)
        {
            Bitmap bitmap = new Bitmap(bitmapSize, bitmapSize);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(fillColor), 0, 0, bitmapSize + 1, bitmapSize + 1);

            graphics.DrawRectangle(pen, 0, 0, bitmapSize - 1, bitmapSize - 1);

            return bitmap;
        }

        private static Bitmap DrawCoordinates(Bitmap bitmap,Font font, Color fontColor, List<Point> vertexes , List<Point> coordinateVertexes )
        {
            for (int i = 0; i < vertexes.Count; i++)
            {
               bitmap = DrawSingleCoordinate(bitmap,font,fontColor, vertexes[i], coordinateVertexes[i]);
            }

            return bitmap;
        }

        private static Bitmap DrawSingleCoordinate(Bitmap bitmap,Font font,Color fontColor, Point vertex, Point coordinates)
        {
            Graphics graphics = Graphics.FromImage(bitmap);

            string text = coordinates.ToString();

            float x = vertex.X;
            float y = vertex.Y;

            SizeF size = graphics.MeasureString(text, font);
            Bitmap stringBitmap = new Bitmap((int)size.Width, (int)size.Height);

            Graphics.FromImage(stringBitmap).DrawString(text, font, new SolidBrush(fontColor), 0, 0);

            stringBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            graphics.DrawImage(stringBitmap, x, y);

            return bitmap;
        }

        private static Bitmap DrawFigureFill(Bitmap bitmap, List<Point> vertexes, Color fillColorBack, Color fillColorFront)
        {
            Graphics graphics = Graphics.FromImage(bitmap);

            PointF[] points = vertexes.Select(vertex => new PointF(vertex.X, vertex.Y)).ToArray();
            
            graphics.FillPolygon(new HatchBrush(HatchStyle.LargeConfetti,fillColorFront,fillColorBack), points);

            return bitmap;
        }


        private Bitmap Draw(Figure figure, int targetSize, bool drawCoordinates)
        {
            return Draw(figure.Vertexes, targetSize, drawCoordinates);
        }

        private Bitmap Draw(List<Point> sourceVertexes, int targetSize, bool drawCoordinates)
        {
            List<Point> vertexes = sourceVertexes.Select(vertex => vertex.Clone()).ToList();

            vertexes = NormalizeVertexes(vertexes);

            int size = GetSize(vertexes);
            float multiplier = (targetSize * 0.8f) / ((float)size);

            vertexes = ScaleVertexes(vertexes, multiplier);

            vertexes = CenterVertexes(vertexes, targetSize);

            Bitmap baseBitmap = GenerateBitmapWithBorder(targetSize, BorderPen, FillColor);
            
            baseBitmap = DrawFigureFill(baseBitmap, vertexes, FigureFillColorBack, FigureFillColorFront);

            baseBitmap = DrawPolygon(baseBitmap, vertexes, EdgePen);

            baseBitmap = DrawVertexes(baseBitmap, vertexes, VertexPen);

            if (drawCoordinates)
            {
                baseBitmap = DrawCoordinates(baseBitmap, Font, TextColor, vertexes, sourceVertexes);
            }

            //Y is inverted because when drawing, the coordinate origin is the upper left corner instead of the lower left.
            baseBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            return baseBitmap;
        }

        #endregion

        public Bitmap DrawThumbnail(Figure figure, int targetSize)
        {
            Bitmap result = Draw(figure, targetSize, false);

            return result;
        }

        public Bitmap DrawSchematic(Figure figure, int targetSize)
        {
            Bitmap result = Draw(figure, targetSize, true);

            return result;
        }
        
        public Bitmap DrawSchematic(List<Point> vertexes, int targetSize)
        {
            Bitmap result = Draw(vertexes, targetSize, true);

            return result;
        }

        public Bitmap DrawResult(ClassificationResult classificationResult, int targetSize)
        {
            if (classificationResult.HasError)
            {
                return GenerateBitmapWithBorder(targetSize, BorderPen, FillColor);
            }

            //Drawing the work area
            List<Point> vertexes = classificationResult.WorkingArea.Vertexes.Select(vertex => vertex.Clone()).ToList();

            int size = GetSize(vertexes);
            float multiplier = (targetSize) / ((float)size);

            vertexes = ScaleVertexes(vertexes, multiplier);

            vertexes = NormalizeVertexes(vertexes);

            vertexes = CenterVertexes(vertexes, targetSize);

            //We must calculate the delta in the work area coordinates due to centering it
            //to apply it to the parts
            Vector deltaCentering = new Vector(classificationResult.WorkingArea.Vertexes.First(),vertexes.First());

            Bitmap baseBitmap = GenerateBitmapWithBorder(targetSize, BorderPen, FillColor);

            baseBitmap = DrawFigureFill(baseBitmap, vertexes, FigureFillColorBack, FigureFillColorFront);

            baseBitmap = DrawPolygon(baseBitmap, vertexes, EdgePen);

            baseBitmap = DrawVertexes(baseBitmap, vertexes, VertexPen);

            //Draw results
            foreach (Part part in classificationResult.Parts)
            {
                Vector placement = new Vector(part.Placement);

                //Draw convex hull
                {
                    List<Point> convexHullVertexes = part.ConvexHullVertexes.Select(vertex => vertex.Clone()).ToList();
                    convexHullVertexes.ForEach(x => x.Sum(placement));

                    convexHullVertexes = ScaleVertexes(convexHullVertexes, multiplier);

                    convexHullVertexes.ForEach(x => x.Sum(deltaCentering));

                    baseBitmap = DrawFigureFill(baseBitmap, convexHullVertexes, ConvexHullFillColorBack, ConvexHullFillColorFront);

                    baseBitmap = DrawPolygon(baseBitmap, convexHullVertexes, ConvexHullPen);

                    baseBitmap = DrawVertexes(baseBitmap, convexHullVertexes, VertexPen);
                }

                //Draw part
                {
                    vertexes = part.Vertexes.Select(vertex => vertex.Clone()).ToList();
                    vertexes.ForEach(x => x.Sum(placement));

                    vertexes = ScaleVertexes(vertexes, multiplier);

                    vertexes.ForEach(x => x.Sum(deltaCentering));

                    baseBitmap = DrawFigureFill(baseBitmap, vertexes, FigureFillColorFront, FigureFillColorFront);

                    baseBitmap = DrawPolygon(baseBitmap, vertexes, EdgePen);

                    baseBitmap = DrawVertexes(baseBitmap, vertexes, VertexPen);
                }
            }


            //Draw Extrapolygons
            foreach (Part part in classificationResult.ExtraPolygons)
            {
                Vector placement = new Vector(part.Placement);
                vertexes = part.Vertexes.Select(vertex => vertex.Clone()).ToList();
                vertexes.ForEach(x => x.Sum(placement));

                vertexes = ScaleVertexes(vertexes, multiplier);

                vertexes.ForEach(x => x.Sum(deltaCentering));

                baseBitmap = DrawPolygon(baseBitmap, vertexes, ExtraPolygonPen);
            }


            //Y is inverted because when drawing, the coordinate origin is the upper left corner instead of the lower left.
            baseBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            //Drawing the parts

            return baseBitmap;
        }
    }
}
