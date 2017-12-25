using System.Windows;
using ProjectEstimate.ViewModels;
using ProjectEstimate.Mongo;
using System;
using System.Windows.Controls;

namespace ProjectEstimate
{
    public partial class MainWindow : Window {

        ProjectInfoViewModel pivm;

        public MainWindow(string projectId)
        {
            InitializeComponent();
            pivm = new ProjectInfoViewModel(projectId, this, functionExpander, featureExpander, extraFeatureExpander) { Parent = this };
            DataContext = pivm;
        }

        public void functionExpander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander exp = sender as Expander;
            var parent = exp.Parent as StackPanel;
            foreach(var child in parent.Children)
            {
                Expander childExp = child as Expander;
                if(childExp != null)
                {
                    if (childExp != exp)
                        childExp.IsExpanded = false;
                }
            }
        }
    }
}
