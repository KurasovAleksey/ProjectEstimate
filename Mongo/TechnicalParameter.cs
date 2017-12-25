using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{
    public class TechnicalParameter
    {
        public string Title { get; set; }

        public double Weight { get; set; }

        public double Value { get; set; }

        public override bool Equals(object obj)
        {
            var parameter = obj as TechnicalParameter;
            return parameter != null &&
                   Title == parameter.Title &&
                   Weight == parameter.Weight;
        }

        public override int GetHashCode()
        {
            var hashCode = -1290770966;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + Weight.GetHashCode();
            return hashCode;
        }
    }
}
