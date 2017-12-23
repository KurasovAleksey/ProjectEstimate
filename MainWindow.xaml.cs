using System.Windows;
using ProjectEstimate.ViewModels;

namespace ProjectEstimate
{
    public partial class MainWindow : Window {

        ProjectInfoViewModel pivm;

        public MainWindow(string projectId = null)
        {
            InitializeComponent();
            pivm = new ProjectInfoViewModel(projectId);
            DataContext = pivm;
        }
    }
}
