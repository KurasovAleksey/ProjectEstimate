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
    }
}

