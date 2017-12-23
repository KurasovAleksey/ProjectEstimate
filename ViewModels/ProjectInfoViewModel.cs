using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;
using System.Windows.Controls;
using ProjectEstimate.Mongo;
using _3DForgeApi.DAL.Model;
using ProjectEstimate.Utils;

namespace ProjectEstimate.ViewModels
{
    public class ProjectInfoViewModel : INotifyPropertyChanged
    {
        private bool isNewProject;

        public DbContext Context { get; set; }

        public ProjectInfoViewModel(string projectId)
        {
            Project project;
            if (projectId == null)
            {
                isNewProject = true;
                project = new Project();
            }
            else
            {
                isNewProject = false;
                var projectRepository = new ProjectRepository(Context);
                project = projectRepository.GetProject(projectId);
            }
            InitWindowContent(project);
        }

        public void InitWindowContent(Project project)
        {
            this.Project = project;
            this.EquipmentList
                = new ObservableCollection<Equipment>(project.EquipmentList);
            this.Materials
                = new ObservableCollection<Material>(project.Materials);
            this.Positions
                = new ObservableCollection<Position>(project.Positions);
            this.ExtraFeatures 
                = new ObservableCollection<ExtraFeature>(project.ExtraFeatures);
            this.Features 
                = new ObservableCollection<Feature>(project.Features);
            this.Functions 
                = new ObservableCollection<Function>(project.Functions);
            this.Marks 
                = new ObservableCollection<Mark>(project.Marks);
            this.TechnicalParameters 
                = new ObservableCollection<TechnicalParameter>(project.TechnicalParameters);
            Estimate = project.Estimate ?? new Estimate();


        }

        private Project project;
        public Project Project
        {

            get
            {
                return project;
            }
            set
            {
                project = value;
                NotifyPropertyChanged("Project");
            }
        }

        private Estimate estimate;
        public Estimate Estimate
        {
            get
            {
                return estimate;
            }
            set
            {
                estimate = value;
                NotifyPropertyChanged("Estimate");
            }
        }

        #region Datagrids

        #region DataGrid current values

        Equipment currentEquipment;
        Material currentMaterial;
        Position currentPosition;
        TechnicalParameter currentTechnicalParameter;
        Function currentFunction;
        Feature currentFeature;
        ExtraFeature currentExtraFeature;
        Mark currentMark;

        public Position CurrentPosition
        {
            get => currentPosition;
            set
            {
                currentPosition = value;
                NotifyPropertyChanged("CurrentPosition");
            }
        }

        public Material CurrentMaterial
        {
            get => currentMaterial;
            set
            {
                currentMaterial = value;
                NotifyPropertyChanged("CurrentMaterial");
            }
        }

        public Equipment CurrentEquipment
        {
            get => currentEquipment;
            set
            {
                currentEquipment = value;
                NotifyPropertyChanged("CurrentEquipment");
            }
        }

        public TechnicalParameter CurrentTechnicalParameter {
            get
            {
                return currentTechnicalParameter;
            }

            set
            {
                currentTechnicalParameter = value;
                NotifyPropertyChanged("CurrentTechnicalParameter");
            }
        }

        public ExtraFeature CurrentExtraFeature {
            get
            {
                return currentExtraFeature;
            }

            set
            {
                currentExtraFeature = value;
            }
        }

        public Feature CurrentFeature {
            get
            {
                return currentFeature;
            }

            set
            {
                currentFeature = value;
            }
        }

        public Mark CurrentMark {
            get
            {
                return currentMark;
            }

            set
            {
                currentMark = value;
            }
        }

        public Function CurrentFunction {
            get
            {
                return currentFunction;
            }

            set
            {
                currentFunction = value;
            }
        }


        #endregion DataGrid current values

        #region Equipment
        public ObservableCollection<Equipment> EquipmentList
        {
            get => project.EquipmentList as ObservableCollection<Equipment>;
            set { project.EquipmentList = value; NotifyPropertyChanged("EquipmentList"); }
        }

        private CommandImpl addEquipmentCommand;
        public CommandImpl AddEquipmentCommand
        {
            get => addEquipmentCommand ?? (addEquipmentCommand = new CommandImpl(
                        obj =>
                        {
                            Equipment equipment = new Equipment();
                            EquipmentList.Add(equipment);
                            NotifyPropertyChanged("EquipmentList");
                        }
                        ));
        }

        private CommandImpl removeEquipmentCommand;
        public CommandImpl RemoveEquipmentCommand
        {
            get => removeEquipmentCommand ?? (removeEquipmentCommand = new CommandImpl(
                        obj =>
                        {
                            if (CurrentEquipment != null)
                            {
                                Project.EquipmentList.Remove(CurrentEquipment);
                                EquipmentList.Remove(CurrentEquipment);
                                NotifyPropertyChanged("EquipmentList");
                            }
                        }
                        ));
        }
        #endregion Equipment

        #region Material
        public ObservableCollection<Material> Materials
        {
            get => project.Materials as ObservableCollection<Material>;
            set { project.Materials = value; NotifyPropertyChanged("Materials"); }
        }

        private CommandImpl addMaterialCommand;
        public CommandImpl AddMaterialCommand
        {
            get => addMaterialCommand ??
                  (addMaterialCommand = new CommandImpl(
                        obj =>
                        {
                            var material = new Material();
                            Materials.Add(material);
                            NotifyPropertyChanged("Materials");
                        }));
        }

        private CommandImpl removeMaterialCommand;
        public CommandImpl RemoveMaterialCommand
        {
            get => removeMaterialCommand ??
                  (removeMaterialCommand = new CommandImpl(
                        obj =>
                        {
                            if (CurrentMaterial != null)
                            {
                                Project.Materials.Remove(CurrentMaterial);
                                Materials.Remove(CurrentMaterial);
                                NotifyPropertyChanged("Materials");
                            }
                        }));
        }
        #endregion Material

        #region Position
        public ObservableCollection<Position> Positions
        {
            get => Project.Positions as ObservableCollection<Position>;
            set { Project.Positions = value; NotifyPropertyChanged("Positions"); }
        }

        private CommandImpl addPositionCommand;
        public CommandImpl AddPositionCommand
        {
            get => addPositionCommand ??
                  (addPositionCommand = new CommandImpl(
                        obj =>
                        {
                            var position = new Position();
                            Positions.Add(position);
                            NotifyPropertyChanged("Position");
                        }
                    ));
        }

        private CommandImpl removePositionCommand;
        public CommandImpl RemoveMilitaryAccountCommand
        {
            get => removePositionCommand ??
                  (removePositionCommand = new CommandImpl(
                        obj =>
                        {
                            if (CurrentPosition != null)
                            {
                                Project.Positions.Remove(CurrentPosition);
                                Positions.Remove(CurrentPosition);
                                NotifyPropertyChanged("Positions");
                            }
                        }
                    ));
        }
        #endregion MiltaryAccount

        #region Function
        public ObservableCollection<Function> Functions
        {
            get => Project.Functions as ObservableCollection<Function>;
            set { Project.Functions = value; NotifyPropertyChanged("Functions"); }
        }

        #endregion

        #region Feature
        public ObservableCollection<Feature> Features
        {
            get => Project.Features as ObservableCollection<Feature>;
            set { Project.Features = value; NotifyPropertyChanged("Features"); }
        }

        #endregion

        #region ExtraFeature
        public ObservableCollection<ExtraFeature> ExtraFeatures
        {
            get => Project.ExtraFeatures as ObservableCollection<ExtraFeature>;
            set { Project.ExtraFeatures = value; NotifyPropertyChanged("ExtraFeatures"); }
        }

        #endregion

        #region TechnicalParameter
        public ObservableCollection<TechnicalParameter> TechnicalParameters
        {
            get => Project.TechnicalParameters as ObservableCollection<TechnicalParameter>;
            set { Project.TechnicalParameters = value; NotifyPropertyChanged("TechnicalParameters"); }
        }

        #endregion

        #region Mark
        public ObservableCollection<Mark> Marks
        {
            get => Project.Marks as ObservableCollection<Mark>;
            set { Project.Marks = value; NotifyPropertyChanged("Marks"); }
        }

        #endregion

        #endregion Datagrids

        #region Buttons Commands
        private CommandImpl declineCommand;
        public CommandImpl DeclineCommand
        {
            get => declineCommand ?? (declineCommand = new CommandImpl(obj =>
            {
                if (isNewProject)
                {
                    InitWindowContent(new Project());
                }
                else
                {
                    string id = Project.Id;
                    var repo = new ProjectRepository(Context);
                    var project = repo.GetProject(id);
                    InitWindowContent(project);
                    NotifyPropertyChanged("Project");
                }
            }));
        }

        private CommandImpl acceptCommand;
        public CommandImpl AcceptCommand
        {
            get => acceptCommand ?? (acceptCommand = new CommandImpl(obj =>
            {
                var repo = new ProjectRepository(Context);
                if (isNewProject)
                {
                    string id = repo.UpsertProject(Project).UpsertedId.AsString;
                    isNewProject = false;
                    Project.Id = id;
                }
                else
                {
                    repo.UpsertProject(Project);

                }
            }));
        }
       

        #endregion Buttons Commands

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        class FunctionChecker
        {
            public FunctionChecker(ProjectInfoViewModel parent, Function item, bool isChecked)
            {
                Item = item;
                this.isChecked = isChecked;
                this.parent = parent;
            }

            ProjectInfoViewModel parent;

            public Function Item { get; set; }

            bool isChecked;
            public bool IsChecked {
                get { return isChecked; }
                set
                {
                    if (!isChecked)
                    {
                        parent.Project.Functions.Add(Item);
                        parent.Project.LOC += Item.LOC;
                        isChecked = true;
                    } else
                    {
                        parent.Project.Functions.Remove(Item);
                        parent.Project.LOC -= Item.LOC;
                    }
                }
            }
        }

        class FeatureChecker
        {
            public FeatureChecker(ProjectInfoViewModel parent, Feature item, bool isChecked)
            {
                Item = item;
                this.isChecked = isChecked;
                this.parent = parent;
            }

            ProjectInfoViewModel parent;

            public Feature Item { get; set; }

            bool isChecked;
            public bool IsChecked
            {
                get { return isChecked; }
                set
                {
                    if (!isChecked)
                    {
                        parent.Project.Features.Add(Item);
                        isChecked = true;
                    }
                    else
                    {
                        parent.Project.Features.Remove(Item);
                    }
                    CalculateCategory();
                }
            }

            public void CalculateCategory()
            {
                int category = 3;
                if (parent.Project.Features.Where(f => f.Category == 3).Any())
                    category = 3;
                if(parent.Project.Features.Where(f => f.Category == 2).Any())
                    category = 2;
                if (parent.Project.Features.Where(f => f.Category == 1).Any())
                    category = 1;
                parent.Project.Category = category;
            }
        }

        class ExtraFeatureChecker
        {
            public ExtraFeatureChecker(ProjectInfoViewModel parent, ExtraFeature item, bool isChecked)
            {
                Item = item;
                this.isChecked = isChecked;
                this.parent = parent;
            }

            ProjectInfoViewModel parent;

            public ExtraFeature Item { get; set; }

            bool isChecked;
            public bool IsChecked
            {
                get { return isChecked; }
                set
                {
                    if (!isChecked)
                    {
                        parent.Project.ExtraFeatures.Add(Item);
                        isChecked = true;
                    }
                    else
                    {
                        parent.Project.ExtraFeatures.Remove(Item);
                    }
                }
            }
        }

    }
}