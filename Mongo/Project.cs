using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{
    public class Project
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }

        public string ApplicationDomain { get; set; }

        public string Info { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime Deadline { get; set; }

        public int LaborContent { get; set; }

        public DateTime Lifetime { get; set; }

        public ICollection<Position> Positions { get; set; }

        public ICollection<Material> Materials { get; set; }

        public ICollection<Equipment> EquipmentList { get; set; }

        public double DevelopmentSquare { get; set; }

        public Product Product { get; set; }

        public Estimate Estimate { get; set; }
    }
}
