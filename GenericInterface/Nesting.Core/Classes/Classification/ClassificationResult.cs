using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Nesting;
using Nesting.Core.Classes.Nesting;

namespace Nesting.Core.Classes.Classification
{
    public class ClassificationResult 
    {
        public ClassifierInformation Classifier { get; set; }

        public WorkingArea WorkingArea { get; set; }

        public List<Part> Parts { get; set; }
        public TimeSpan? TimeTaken { get; set; }
        public Exception Error { get; set; }
        public bool HasError { get; set; }
        public float RemainingArea { get; set; }
        
        public List<Part> ExtraPolygons { get; set; } 

        protected ClassificationResult()
        {
            Parts = new List<Part>();
            ExtraPolygons = new List<Part>();
        }

        public ClassificationResult(ClassifierInformation classifierInformation): this()
        {
            this.Classifier = classifierInformation;
        }
        
        public ClassificationResult Clone()
        {
            ClassificationResult clone = new ClassificationResult()
            {
                ExtraPolygons = this.ExtraPolygons.Select(x => x.Clone()).ToList(),
                Parts = this.Parts.Select(x => x.Clone()).ToList(),
                WorkingArea = this.WorkingArea.Clone(),
                Classifier = this.Classifier,
                Error = this.Error,
                HasError = this.HasError,
                RemainingArea = this.RemainingArea,
                TimeTaken = this.TimeTaken
            };

            return clone;
        }

        public void AddSubResult(ClassificationResult subResult, Point subResultOrigin)
        {
            foreach (Part polygon in subResult.ExtraPolygons)
            {
                Part clone = polygon.Clone();
                clone.Placement = clone.Placement + subResultOrigin;
                ExtraPolygons.Add(clone);
            }

            foreach (var part in subResult.Parts)
            {
                Part placedPart = part.Clone();

                Point placementPoint = part.Placement + subResultOrigin;

                placedPart.Place(placementPoint);

                Parts.Add(placedPart);
            }
        }

        public override bool Equals(object obj)
        {
            ClassificationResult other = obj as ClassificationResult;

            if (other == null)
            {
                return false;
            }

            bool result = this.WorkingArea.Equals(other.WorkingArea);

            result = result && this.Parts.Count == other.Parts.Count;

            foreach (Part part in this.Parts)
            {
                result = result && other.Parts.Any(x => x.Equals(part));
            }

            return result;
        }
    }
}
