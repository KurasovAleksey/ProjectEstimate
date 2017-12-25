using ProjectEstimate.Mongo;
using ProjectEstimate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectEstimate
{
    /// <summary>
    /// Логика взаимодействия для CompetitivenessWindow.xaml
    /// </summary>
    public partial class CompetitivenessWindow : Window
    {
        public CompetitivenessWindow(DbContext context)
        {
            InitializeComponent();
            DataContext = new CompetitorsViewModel(context) { Parent = this };
        }

        public Project Standart { get; set; }

        public Project Base { get; set; }

        public void SwapPanels()
        {
            stkPanelStandart.Visibility = Visibility.Hidden;
            stkPanelBase.Visibility = Visibility.Visible;
        }

    }
}
