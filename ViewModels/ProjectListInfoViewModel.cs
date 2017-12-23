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
using _3DForgeApi.DAL.Model;

namespace ProjectEstimate.ViewModels
{
    public class ProjectListInfoViewModel : INotifyPropertyChanged
    { 

        public ProjectListInfoViewModel()
        {
            projectListInfo = new ObservableCollection<ProjectInfoDto>();
        }

        public DbContext Context { get; set; }

        public string TitleFilter { get; set; }

        public string AppDomainFilter { get; set; }

        public string InfoFilter { get; set; }

        ObservableCollection<ProjectInfoDto> projectListInfo;
        public ObservableCollection<ProjectInfoDto> ProjectListInfo
        {
            get => projectListInfo;
            set
            {
                projectListInfo = value;
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
                var window = new MainWindow();
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
                          var window = new MainWindow() { ProjectId = SelectedItem.Id };
                          window.Show();
                      }
                  }));
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
                                ProjectListInfo.Remove(SelectedItem);
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
