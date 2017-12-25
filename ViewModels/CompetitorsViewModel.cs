using ProjectEstimate.Mongo;
using ProjectEstimate.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.ViewModels
{
    public class CompetitorsViewModel
    {
        public CompetitorsViewModel(DbContext context)
        {
            Context = context;
            ProjectRepository pr = new ProjectRepository(context);
            var list = from p in pr.GetAllProjects()
                       select new ProjectInfoDto()
                       { Id = p.Id, Title = p.Title, ApplicationDomain = p.ApplicationDomain };
            ProjectsListInfo = new ObservableCollection<ProjectInfoDto>(list.ToList());
        }

        public DbContext Context { get; set; }

        public CompetitivenessWindow Parent { get; set; }

        public Project StandartProject { get; set; }

        public Project BaseProject { get; set; }

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

        private CommandImpl chooseStandart;
        public CommandImpl ChooseStandart
        {
            get => chooseStandart ?? (chooseStandart = new CommandImpl(obj =>
            {
                if (SelectedItem == null)
                    return;
                ProjectRepository pr = new ProjectRepository(Context);
                Parent.Standart = pr.GetProject(SelectedItem.Id);
                Parent.SwapPanels();
            }));
        }

        private CommandImpl chooseBase;
        public CommandImpl ChooseBase
        {
            get => chooseBase ?? (chooseBase = new CommandImpl(obj =>
            {
                if (SelectedItem == null)
                    return;
                ProjectRepository pr = new ProjectRepository(Context);
                Parent.Base = pr.GetProject(SelectedItem.Id);
                Parent.Close();
            }));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
