using System.Windows;
using ProjectEstimate.ViewModels;

namespace ProjectEstimate
{
    public partial class MainWindow : Window {

        ProjectInfoViewModel pivm;

        public string ProjectId { get; set; } = null;

        public MainWindow()
        {
            InitializeComponent();
            pivm = new ProjectInfoViewModel(ProjectId);
            DataContext = pivm;
        }
    }
}
