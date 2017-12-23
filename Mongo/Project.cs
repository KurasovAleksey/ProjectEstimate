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
        public Project()
        {
            Positions = new HashSet<Position>();
            EquipmentList = new HashSet<Equipment>();
            Materials = new HashSet<Material>();
            Functions = new HashSet<Function>();
            ExtraFeatures = new HashSet<ExtraFeature>();
            Features = new HashSet<Feature>();
            TechnicalParameters = new HashSet<TechnicalParameter>();
            Marks = new HashSet<Mark>();
            Estimate = new Estimate();
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }

        public string ApplicationDomain { get; set; }

        public string Info { get; set; }

        public DateTime StartingDate { get; set; } = DateTime.Now;

        public DateTime Deadline { get; set; } = DateTime.Now;

        public int LOC { get; set; } = 0;

        public int Category { get; set; }

        public DateTime Lifetime { get; set; }

        public ICollection<Position> Positions { get; set; }

        public ICollection<Material> Materials { get; set; }

        public ICollection<Equipment> EquipmentList { get; set; }

        public ICollection<Function> Functions { get; set; }

        public ICollection<Feature> Features { get; set; }

        public ICollection<ExtraFeature> ExtraFeatures { get; set; }

        public double DevelopmentSquare { get; set; }

        public ICollection<TechnicalParameter> TechnicalParameters { get; set; }

        public ICollection<Mark> Marks { get; set; }

        public Estimate Estimate { get; set; }
    }
}
