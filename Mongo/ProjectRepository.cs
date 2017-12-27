using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.IO;
using ProjectEstimate.Mongo;

namespace ProjectEstimate.Mongo
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

        public void UpsertProject(Project project)
        {
            if (project.StartingDate > project.Deadline)
                throw new Exception("Некорректный ввод дат");
            if (String.IsNullOrWhiteSpace(project.Title))
                throw new Exception("Введите название проекта");
            if (project.Id == null)
                 _context.Projects.InsertOne(project, null);
            else
            {
                var filter = Builders<Project>.Filter.Eq("Id", project.Id);
                _context.Projects.DeleteOne(filter);
                _context.Projects.InsertOne(project, null);
            }
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
