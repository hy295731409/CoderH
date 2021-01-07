using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._7
{
    public class SequenceDagramConfig
    {
        /// <summary>
        /// 药品
        /// </summary>
        public IList<SequenceDagramDrug> Drugs { get; set; }
        /// <summary>
        /// 检验结果项目
        /// </summary>
        public IList<SequenceDagramLabSub> LabSubs { get; set; }
        /// <summary>
        /// 显示项目
        /// </summary>
        public ShowOrHide ShowOrHide { get; set; }
        /// <summary>
        /// 药理类
        /// </summary>
        public IList<DrugClass> DrugClass { get; set; }
        /// <summary>
        /// 检验结果分类
        /// </summary>
        public IList<int> LabClass { get; set; }
    }

    public class ShowOrHide
    {
        public bool Tw { get; set; }
        public bool Mb { get; set; }
        public bool Score { get; set; }
        public bool Glu { get; set; }
        public bool Respiratory { get; set; }
        public bool OutPut { get; set; }
        public bool InPut { get; set; }
        public bool BloodPressure { get; set; }
        public bool Surgery { get; set; }
        public bool Sensitivity { get; set; }
        public bool Anti { get; set; }
        public bool Stool { get; set; }
    }

    public class DrugClass
    {
        public int ClassId { get; set; }
        public string ClassTitle { get; set; }
    }

    public class SequenceDagramDrug
    {
        public int MatchScheme { get; set; }
        public string DrugIndex { get; set; }
        public string DrugCode { get; set; }
        public string DrugName { get; set; }
        public string DrugForm { get; set; }
        public string DrugSpec { get; set; }
        public string DrugSCCJ { get; set; }
        public string SearchCode { get; set; }
        public string DrugGroupName { get; set; }
        public string ClassTitle { get; set; }
    }

    public class SequenceDagramLabSub
    {
        public string LabItemCode { get; set; }
        public string LabItemName { get; set; }
    }
    public class LabClass
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public bool Checked { get; set; }
    }
}
