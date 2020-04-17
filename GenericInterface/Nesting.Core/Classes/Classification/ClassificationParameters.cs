using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nesting.Core.Classes.Nesting;

namespace Nesting.Core.Classes
{
    public class ClassificationParameters
    {
        public WorkingArea WorkingArea { get; private set; }

        public List<Part> Parts { get; private set; }

        public ClassificationParameters(WorkingArea workingArea, List<Part> parts)
        {
            if (workingArea == null)
            {
                throw new Exception("Error creating ClassificationParameters: WorkingArea is null!");
            }

            if (workingArea.Vertexes.Count < 3)
            {
                throw new Exception("Error creating ClassificationParameters: WorkingArea has a wrong number of vertexes!");
            }

            if (parts == null || parts.Count == 0)
            {
                throw new Exception("Error creating ClassificationParameters: Parts list is null or empty!");
            }

            //We check if parts can't fit in the working area (too big)

            this.WorkingArea = workingArea.Clone();

            this.WorkingArea.Parts.Clear();

            double available = workingArea.GetTotalArea();

            this.Parts = new List<Part>();

            foreach (Part part in parts)
            {
                Part clone = (Part)part.Clone();
                clone.UnPlace();

                if (clone.Vertexes.Count < 3)
                {
                    throw new Exception("Error creating ClassificationParameters: Part has a wrong number of vertexes!");
                }

                available = available - clone.GetTotalArea();

                Parts.Add(clone);
            }
        }

        #region Implementation of ICloneable

        public ClassificationParameters Clone()
        {
            List<Part> parts = Parts.Select(x => x.Clone()).ToList();

            return new ClassificationParameters(this.WorkingArea.Clone(),parts);
        }

        #endregion
    }
}
