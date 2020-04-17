using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes.Geometry;

namespace Nesting.Core.Classes.Nesting
{
    public class WorkingArea : Figure
    {
        public List<Part> Parts = new List<Part>();

        public WorkingArea Clone()
        {
            WorkingArea newWorkingArea = new WorkingArea();

            newWorkingArea.Description = Description;
            newWorkingArea.Name = Name;
            newWorkingArea.Id = Id;

            newWorkingArea.Vertexes = Vertexes.Select(x=>x.Clone()).ToList();

            return newWorkingArea;
        }

    }
}
