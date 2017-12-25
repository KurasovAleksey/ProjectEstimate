using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectEstimate.Utils;
using ProjectEstimate;
using ProjectEstimate.Mongo;

namespace ProjectEstimate.ViewModels
{
    public class ProjectListInfoViewModel : INotifyPropertyChanged
    { 

        public ProjectListInfoViewModel(DbContext context)
        {
            Context = context;
            InitList();
        }

        public void InitList()
        {
            ProjectRepository pr = new ProjectRepository(Context);
            var list = (from p in pr.GetAllProjects()
                       select new ProjectInfoDto()
                       { Id = p.Id, Title = p.Title, ApplicationDomain = p.ApplicationDomain }).ToList();
            ProjectsListInfo = new ObservableCollection<ProjectInfoDto>(list);
        }

        public DbContext Context { get; set; }

        public string TitleFilter { get; set; }

        public string AppDomainFilter { get; set; }

        public string InfoFilter { get; set; }

        ObservableCollection<ProjectInfoDto> projectsListInfo;
        public ObservableCollection<ProjectInfoDto> ProjectsListInfo
        {
            get => projectsListInfo;
            set
            {
                projectsListInfo = value;
                OnPropertyChanged("ProjectListInfo");
            }
        }

        private ProjectInfoDto selectedItem;
        public ProjectInfoDto SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private CommandImpl closeStateCommand;
        public CommandImpl CloseStateCommand
        {
            get => closeStateCommand ?? (closeStateCommand = new CommandImpl(obj =>
            {
                Environment.Exit(0);
            }));
        }

        private CommandImpl addNewProject;
        public CommandImpl AddNewProject
        {
            get => addNewProject ?? (addNewProject = new CommandImpl(obj =>
            {
                var window = new MainWindow(null);
                window.Closed += Window_Closed;
                window.Show();
            }));
        }

       

        #region Context Menu Items commands
        private CommandImpl editSelectedItemCommand;
        public CommandImpl EditSelectedItemCommand
        {
            get => editSelectedItemCommand ??
                  (editSelectedItemCommand = new CommandImpl(obj =>
                  {
                      if (SelectedItem != null)
                      {
                          var window = new MainWindow(SelectedItem.Id);
                          window.Closed += Window_Closed;
                          window.Show();
                      }
                  }));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            InitList();
        }

        private CommandImpl removeSelectedItemCommand;
        public CommandImpl RemoveSelectedItemCommand
        {
            get => removeSelectedItemCommand ??
                  (removeSelectedItemCommand = new CommandImpl(
                        obj =>
                        {
                            if (SelectedItem != null)
                            {
                                var repo = new ProjectRepository(Context);
                                repo.RemoveProject(SelectedItem.Id);
                                ProjectsListInfo.Remove(SelectedItem);
                            }
                        }
                    ));
        }
        #endregion Context Menu Items commands

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
