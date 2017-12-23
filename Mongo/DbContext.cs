using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Mongo
{
    public class DbContext
    {
        private readonly IMongoDatabase database = null;

        public DbContext()
        {
            var client = new MongoClient("mongodb://localhost");
            if (client != null)
            {
                database = client.GetDatabase("Projects");
            }
        }

        public IMongoCollection<Project> Projects
        {
            get => database.GetCollection<Project>("Projects");
        }

        public IMongoCollection<Function> Functions
        {
            get => database.GetCollection<Function>("Functions");
        }

        public IMongoCollection<Feature> Features
        {
            get => database.GetCollection<Feature>("Features");
        }

        public IMongoCollection<ExtraFeature> ExtraFeatures
        {
            get => database.GetCollection<ExtraFeature>("ExtraFeatures");
        }
    }
}

