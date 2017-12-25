using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{

    public class Material
    {
        public string Title { get; set; }

        public double Cost { get; set; }

        public int Quantity { get; set; }

        public string Unit { get; set; }

        public override bool Equals(object obj)
        {
            var material = obj as Material;
            return material != null &&
                   Title == material.Title;
        }

        public override int GetHashCode()
        {
            return 434131217 + EqualityComparer<string>.Default.GetHashCode(Title);
        }
    }
}
