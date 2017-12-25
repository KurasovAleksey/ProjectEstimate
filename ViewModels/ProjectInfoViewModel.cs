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
using ProjectEstimate.Utils;
using System.IO;

namespace ProjectEstimate.ViewModels
{
    public class ProjectInfoViewModel : INotifyPropertyChanged
    {
        private bool isNewProject;

        public DbContext Context { get; set; }

        public MainWindow Parent { get; set; }

        Dictionary<int, string> functionCategoryDictionary;
        Dictionary<int, string> featureCategoryDictionary;

        public ProjectInfoViewModel(string projectId, MainWindow parent, params Expander[] expanders)
        {
            Parent = parent;
            
            Context = new DbContext();
            functionCategoryDictionary = new Dictionary<int, string>();
            functionCategoryDictionary.Add(1, "Ввод, анализ входной информации, генерация кодов и процессор входного языка");
            functionCategoryDictionary.Add(2, "Формирование, введение и обслуживание баз данных (БД)");
            functionCategoryDictionary.Add(3, "Формирование и обработка файлов");
            functionCategoryDictionary.Add(4, "Генерация программ и ПО, а также настройка ПО");
            functionCategoryDictionary.Add(5, "Управление ПО, компонентами ПО и внешними устройствами");
            functionCategoryDictionary.Add(6, "Тестирование, проведение тестовых испытаний прикладных программ, вспомогательные функции");
            functionCategoryDictionary.Add(7, "Расчетные задачи, формирование и вывод на внешние носители документов сложной формы и файлов");
            functionCategoryDictionary.Add(8, "Создание Internet-портала");

            featureCategoryDictionary = new Dictionary<int, string>();
            featureCategoryDictionary.Add(1, "Категория 1");
            featureCategoryDictionary.Add(2, "Категория 2");
            featureCategoryDictionary.Add(3, "Категория 3");

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
            InitWindowContent(project, expanders);
        }

        public void InitWindowContent(Project project, params Expander[] expanders)
        {
            var functionsExpander = expanders[0];
            var featuresExpander = expanders[1];
            var extraFeaturesExpander = expanders[2];

            this.Project = project;
            this.EquipmentList
                = new ObservableCollection<Equipment>(project.EquipmentList);
            this.Materials
                = new ObservableCollection<Material>(project.Materials);
            this.Positions
                = new ObservableCollection<Position>(project.Positions);
            
            this.Marks 
                = new ObservableCollection<Mark>(project.Marks);
            this.TechnicalParameters 
                = new ObservableCollection<TechnicalParameter>(project.TechnicalParameters);
            Estimate = new Estimate();

            ProjectRepository pr = new ProjectRepository(Context);
            Thickness checkBoxMargin = new Thickness(20, 0, 0, 0);

            var functionsList = pr.GetAllFunctions().GroupBy(f => f.CategoryId).OrderBy(g => g.Key);
            StackPanel functionsStackPanel = functionsExpander.Content as StackPanel;
            foreach(var group in functionsList)
            {
                Expander expander = new Expander();
                expander.Header = functionCategoryDictionary[group.Key];
                expander.Expanded += Parent.functionExpander_Expanded;
                StackPanel stkPanel = new StackPanel();
                foreach (var f in group)
                {
                    FunctionChecker funcChecker = new FunctionChecker(this, f, Project.Functions.Contains(f));
                    CheckBox chBox = new CheckBox() { DataContext = funcChecker, Margin=checkBoxMargin };
                    chBox.SetBinding(CheckBox.IsCheckedProperty, "IsChecked");
                    chBox.SetBinding(CheckBox.ContentProperty, "Item.Title");
                    stkPanel.Children.Add(chBox);
                }
                expander.Content = stkPanel;

                functionsStackPanel.Children.Add(expander);
               


            }
            functionsExpander.Content = functionsStackPanel;

            var featuresList = pr.GetAllFeatures().GroupBy(f => f.Category).OrderBy(g => g.Key);
            StackPanel featuresStackPanel = featuresExpander.Content as StackPanel;
            foreach (var group in featuresList)
            {
                Expander expander = new Expander();
                expander.Header = featureCategoryDictionary[group.Key];
                expander.Expanded += Parent.functionExpander_Expanded;
                StackPanel stkPanel = new StackPanel();
                foreach (var f in group)
                {
                    FeatureChecker featChecker = new FeatureChecker(this, f, Project.Features.Contains(f));
                    CheckBox chBox = new CheckBox() { DataContext = featChecker, Margin = checkBoxMargin };
                    chBox.SetBinding(CheckBox.IsCheckedProperty, "IsChecked");
                    chBox.SetBinding(CheckBox.ContentProperty, "Item.Title");
                    stkPanel.Children.Add(chBox);
                }
                expander.Content = stkPanel;

                featuresStackPanel.Children.Add(expander);

            }
            featuresExpander.Content = featuresStackPanel;

            var extraFeaturesList = pr.GetAllExtraFeatures().OrderBy(ef => ef.Id);
            StackPanel extraFeaturesStackPanel = extraFeaturesExpander.Content as StackPanel;
            foreach (var f in extraFeaturesList)
            {
                ExtraFeatureChecker extraFeatChecker = new ExtraFeatureChecker(this, f, Project.ExtraFeatures.Contains(f));
                CheckBox chBox = new CheckBox() { DataContext = extraFeatChecker, Margin = checkBoxMargin };
                chBox.SetBinding(CheckBox.IsCheckedProperty, "IsChecked");
                chBox.SetBinding(CheckBox.ContentProperty, "Item.Title");
                extraFeaturesStackPanel.Children.Add(chBox);
            }
            extraFeaturesExpander.Content = extraFeaturesStackPanel;
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

        private int employeeQuantity;
        public int EmployeeQuantity
        {
            get
            {
                return employeeQuantity;
            }
            set
            {
                employeeQuantity = value;
                NotifyPropertyChanged("EmployeeQuantity");
            }
        }

        private double expensivesSum;
        public double ExpensivesSum
        {
            get
            {
                return expensivesSum;
            }
            set
            {
                expensivesSum = value;
                NotifyPropertyChanged("ExpensivesSum");
            }
        }

        public Coefficients Coefficients
        {
            get => Coefficients.Instance;
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
            get => Project.EquipmentList as ObservableCollection<Equipment>;
            set { Project.EquipmentList = value; NotifyPropertyChanged("EquipmentList"); }
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
            get => Project.Materials as ObservableCollection<Material>;
            set { Project.Materials = value; NotifyPropertyChanged("Materials"); }
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
                            NotifyPropertyChanged("Positions");
                        }
                    ));
        }

        private CommandImpl removePositionCommand;
        public CommandImpl RemovePositionCommand
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

        #region TechnicalParameter
        public ObservableCollection<TechnicalParameter> TechnicalParameters
        {
            get => Project.TechnicalParameters as ObservableCollection<TechnicalParameter>;
            set { Project.TechnicalParameters = value; NotifyPropertyChanged("TechnicalParameters"); }
        }

        private CommandImpl addTechnicalParameterCommand;
        public CommandImpl AddTechnicalParameterCommand
        {
            get => addTechnicalParameterCommand ??
                  (addTechnicalParameterCommand = new CommandImpl(
                        obj =>
                        {
                            var technicalParameter = new TechnicalParameter();
                            TechnicalParameters.Add(technicalParameter);
                            NotifyPropertyChanged("TechnicalParameters");
                        }
                    ));
        }

        private CommandImpl removeTechnicalParameterCommand;
        public CommandImpl RemoveTechnicalParameterCommand
        {
            get => removeTechnicalParameterCommand ??
                  (removeTechnicalParameterCommand = new CommandImpl(
                        obj =>
                        {
                            if (CurrentTechnicalParameter != null)
                            {
                                Project.TechnicalParameters.Remove(CurrentTechnicalParameter);
                                TechnicalParameters.Remove(CurrentTechnicalParameter);
                                NotifyPropertyChanged("TechnicalParameters");
                            }
                        }
                    ));
        }

        #endregion

        #region Mark
        public ObservableCollection<Mark> Marks
        {
            get => Project.Marks as ObservableCollection<Mark>;
            set { Project.Marks = value; NotifyPropertyChanged("Marks"); }
        }

        private CommandImpl addMarkCommand;
        public CommandImpl AddMarkCommand
        {
            get => addMarkCommand ??
                  (addMarkCommand = new CommandImpl(
                        obj =>
                        {
                            var mark = new Mark();
                            Marks.Add(mark);
                            NotifyPropertyChanged("Marks");
                        }
                    ));
        }

        private CommandImpl removeMarkCommand;
        public CommandImpl RemoveMarkCommand
        {
            get => removeMarkCommand ??
                  (removeMarkCommand = new CommandImpl(
                        obj =>
                        {
                            if (CurrentMark != null)
                            {
                                Project.Marks.Remove(CurrentMark);
                                Marks.Remove(CurrentMark);
                                NotifyPropertyChanged("Marks");
                            }
                        }
                    ));
        }

        #endregion

        #endregion Datagrids

        #region Buttons Commands

        private CommandImpl calculateOptimalEmployeeQuantity;
        public CommandImpl CalculateOptimalEmployeeQuantity
        {
            get => calculateOptimalEmployeeQuantity ?? (calculateOptimalEmployeeQuantity = new CommandImpl(obj =>
            {
                EmployeeQuantity = Calculator.CalculateExecutorsQuantity(Project);
            }));
        }

        private CommandImpl calculateExpensivesSum;
        public CommandImpl CalculateExpensivesSum
        {
            get => calculateExpensivesSum ?? (calculateExpensivesSum = new CommandImpl(obj =>
            {
                ExpensivesSum = Calculator.GetExpenseOverall(Project);
            }));
        }

        private CommandImpl saveCommand;
        public CommandImpl SaveCommand
        {
            get => saveCommand ?? (saveCommand = new CommandImpl(obj =>
            {
                try
                {
                    var repo = new ProjectRepository(Context);
                    if (isNewProject)
                    {
                        repo.UpsertProject(Project);
                        isNewProject = false;
                    }
                    else
                    {
                        repo.UpsertProject(Project);
                    }
                } catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }));
        }

        private CommandImpl closeCommand;
        public CommandImpl CloseCommand
        {
            get => closeCommand ?? (closeCommand = new CommandImpl(obj =>
            {
                Parent.Close();
            }));
        }

        private CommandImpl calculateLaboriousness;
        public CommandImpl CalculateLaboriousness
        {
            get => calculateLaboriousness ?? (calculateLaboriousness = new CommandImpl(obj =>
            {
                Estimate.Laboriousness = Calculator.CalculateLaboriousness(Project);
                NotifyPropertyChanged("Estimate");
            }));
        }

        private CommandImpl calculateCostPrice;
        public CommandImpl CalculateCostPrice
        {
            get => calculateCostPrice ?? (calculateCostPrice = new CommandImpl(obj =>
            {
                Estimate.CostPrice = Calculator.CalculateCostPrice(Project);
                NotifyPropertyChanged("Estimate");
            }));
        }

        private CommandImpl calculateEfficiency;
        public CommandImpl CalculateEfficiency
        {
            get => calculateEfficiency ?? (calculateEfficiency = new CommandImpl(obj =>
            {
                Estimate.EconomicEfficiency = Calculator.CalculateEconomicEfficiency(Project);
                NotifyPropertyChanged("Estimate");
            }));
        }

        private CommandImpl calculateConcurrency;
        public CommandImpl CalculateConcurrency
        {
            get => calculateConcurrency ?? (calculateConcurrency = new CommandImpl(obj =>
            {
                CompetitivenessWindow cw = new CompetitivenessWindow(Context);
                cw.ShowDialog();
                if (cw.Standart == null || cw.Base == null)
                    return;
                Estimate.Concurrency = Calculator.CalculateConcurrency(Project, cw.Base, cw.Standart);
                NotifyPropertyChanged("Estimate");
            }));
        }

        #endregion Buttons Commands

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class FunctionChecker
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
                        isChecked = false;
                    }
                    parent.NotifyPropertyChanged("Project");
                }
            }
        }

        public class FeatureChecker
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
                        isChecked = false;
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
                parent.NotifyPropertyChanged("Project");
            }
        }

        public class ExtraFeatureChecker
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
                        isChecked = false;
                    }
                }
            }
        }

    }
}