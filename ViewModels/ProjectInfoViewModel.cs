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

        Equipment currentEquipment;
        Material currentMaterial;
        Position currentPosition;

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
            Estimate = project.Estimate ?? new Estimate();
            Product = project.Product ?? new Product();
            

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

        private Product product;
        public Product Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                NotifyPropertyChanged("Product");
            }
        }

        #region Datagrids

        #region DataGrid current values

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
        public object _currentEquipment { get; private set; }
        #endregion Buttons Commands

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}