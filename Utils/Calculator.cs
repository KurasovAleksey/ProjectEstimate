using ProjectEstimate.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEstimate.Utils
{
    public static class Calculator
    {
        public static double CalculateEconomicEfficiency(Project project)
        {
            return (CalculateSellingPrice(project) - CalculateCostPrice(project))
                    / GetEquipmentExpenseOverall(project);
        }

        public static double CalculateCostPrice(Project project)
        {
            Coefficients coeffs = Coefficients.Instance;
            double salary = GetEmployeesSalaryOverall(project);

            return GetExpenseOverall(project)
                    + salary
                    + salary * coeffs.ExtraSalary
                    + (salary + salary * coeffs.ExtraSalary) * coeffs.SocialNeeds
                    + salary * coeffs.Overheads;
        }

        public static double CalculateLaboriousness(Project project)
        {
            double labourNormal;
            int functionSum = project.LOC;

            switch (project.Category)
            {
                case 1:
                    labourNormal = Math.Round(0.12 * Math.Pow(functionSum, 0.92));
                    break;

                case 2:
                    labourNormal = Math.Round(0.105 * Math.Pow(functionSum, 0.915));
                    break;

                case 3:
                    labourNormal = Math.Round(0.092 * Math.Pow(functionSum, 0.91));
                    break;

                default:
                    return Double.NaN;
            }

            
            double k = 1;
            foreach (var extraFeature in project.ExtraFeatures)
            {
                k += extraFeature.Coefficient;
            }
            switch (project.Features.Count)
            {
                case 0:
                    break;

                case 1:
                    break;

                case 2:
                    k += 0.12;
                    break;

                case 3:
                    k += 0.18;
                    break;

                default:
                    k += 0.26;
                    break;
            }

            return Math.Round(labourNormal * k);
        }

        public static int CalculateExecutorsQuantity(Project project)
        {
            return (int)Math.Round(CalculateLaboriousness(project) / (22 * 6));
        }

        public static double CalculateConcurrency(Project newProject, Project baseProject, Project standartProject)
        {
            double kTechnical, kRating, kPrice;
            double kNew = 0, kBase = 0;

            var newParams = newProject.TechnicalParameters;
            var baseParams = baseProject.TechnicalParameters;
            var stdParams = standartProject.TechnicalParameters;

            TechnicalParameter baseParam, stdParam;

            foreach (var newParam in newParams)
            {
                baseParam = baseParams.Where(p => p.Title == newParam.Title).FirstOrDefault();
                stdParam = stdParams.Where(p => p.Title == newParam.Title).FirstOrDefault();
                if (baseParam == null || stdParam == null)
                    continue;
                kNew += newParam.Weight * (newParam.Value / stdParam.Value);
                kBase += newParam.Weight * (baseParam.Value / stdParam.Value);
            }
            kTechnical = kNew / kBase;

            kNew = kBase = 0;
            var newMarks = newProject.Marks;
            var baseMarks = baseProject.Marks;

            Mark baseMark;

            foreach (var newMark in newMarks)
            {
                baseMark = baseMarks.Where(m => m.Title == newMark.Title).FirstOrDefault();
                if (baseMark == null)
                    continue;
                kNew += newMark.Value;
                kBase += baseMark.Value;
            }
            kRating = kNew / kBase;

            kPrice = CalculateContractPrice(newProject) / CalculateContractPrice(baseProject);

            return kTechnical * kRating / kPrice;
        }

        #region Utils
        public static double CalculateContractPrice(Project project)
        {
            return CalculateCostPrice(project) * (1 + Coefficients.Instance.Rentability);
        }

        public static double CalculateSellingPrice(Project project)
        {
            return CalculateContractPrice(project) * (1 + Coefficients.Instance.TransportExpenses + Coefficients.Instance.TradeOrgsMarkUp);
        }

        public static double GetEmployeesSalaryOverall(Project project)
        {
            double sum = 0;
            foreach (var position in project.Positions)
                sum += position.Salary * position.Quantity;
            DateTime f1 = project.StartingDate;
            DateTime f2 = project.Deadline;
            int months = ((f2.Year - f1.Year) * 12) + (f2.Month-f1.Month);
            return sum * months;
        }

        public static double GetExpenseOverall(Project project)
        {
            double sum = 0;
            foreach (var material in project.Materials)
                sum += (material.Cost * material.Quantity);
            foreach (var equipment in project.EquipmentList)
                sum += (equipment.Cost * equipment.Quantity);

            return sum * (Coefficients.Instance.TradeProcurementExpenses + 1);
        }

        public static double GetEquipmentExpenseOverall(Project project)
        {
            double sum = 0;
            foreach (var equipment in project.EquipmentList)
                sum += (equipment.Cost * equipment.Quantity);

            return sum * (Coefficients.Instance.TradeProcurementExpenses + 1);
        }
        #endregion

    }
}
