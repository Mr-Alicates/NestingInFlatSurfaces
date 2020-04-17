using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Nesting;
using Nesting.Core.Classes.Nesting;

namespace Nesting.Core.Interfaces
{
    public interface INestingManager
    {
        #region WorkingArea functions

        bool IsRectangle(WorkingArea area, out float width, out float height);

        bool IsRightTriangle(Part part);

        #endregion

        void GetRectangleBoxOfPart(Part part, out float width, out float height);

        void GetRectangleBoxOfParts(List<Part> parts, out float width, out float height);

        Part CalculateFlippedRightTriangle(Part triangle);

        Part CalculateRectangle(float width, float height, Point coordinates);

        WorkingArea CalculateWorkingArea(float width, float height);

    }
}
