using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{
    public class ExtraFeature
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }

        public double Coefficient { get; set; }

        public bool Equals(ExtraFeature other)
        {
            if (this.Id == other.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
