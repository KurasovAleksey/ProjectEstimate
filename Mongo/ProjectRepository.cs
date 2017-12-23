﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.IO;
using ProjectEstimate.Mongo;

namespace _3DForgeApi.DAL.Model
{
    public class ProjectRepository
    {
        private readonly DbContext _context = null;

        public ProjectRepository(DbContext context)
        {
            _context = context;
        }

        #region Project

        public IEnumerable<Project> GetAllProjects()
        {
            return _context.Projects.Find(_ => true).ToEnumerable();
        }

        public Project GetProject(string id)
        {
            var filter = Builders<Project>.Filter.Eq("Id", id);
            return _context.Projects
                .Find(filter)
                .FirstOrDefault();
        }

        public ReplaceOneResult UpsertProject(Project project)
        {
            return _context.Projects.ReplaceOne(new BsonDocument("Id", project.Id),
                 project, new UpdateOptions() { IsUpsert = true });
        }

        public DeleteResult RemoveProject(string id)
        {
            var filter = Builders<Project>.Filter.Eq("Id", id);
            return _context.Projects.DeleteOne(filter);
        }

        #endregion

        #region Function
        public IEnumerable<Function> GetAllFunctions()
        {
            return _context.Functions.Find(_ => true).ToEnumerable();
        }
        #endregion

        #region Feature
        public IEnumerable<Feature> GetAllFeatures()
        {
            return _context.Features.Find(_ => true).ToEnumerable();
        }
        #endregion

        #region ExtraFeature
        public IEnumerable<ExtraFeature> GetAllExtraFeatures()
        {
            return _context.ExtraFeatures.Find(_ => true).ToEnumerable();
        }
        #endregion

    }
}
