using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp4._7.test
{
    /// <summary>
    /// 电子药历-导出完整结构
    /// </summary>
    public class MedRecord
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        public PatBaseInfo BaseInfo { get; set; }

        #region 病史及用药史
        /// <summary>
        /// 高龄病人
        /// </summary>
        public int AdvancedAge { get; set; }
        /// <summary>
        /// 肝功异常
        /// </summary>
        public int LiverFunction { get; set; }
        /// <summary>
        /// 肾功异常
        /// </summary>
        public int RenalFunction { get; set; }
        /// <summary>
        /// 主诉
        /// </summary>
        public string MainSuit { get; set; }
        /// <summary>
        /// 现病史
        /// </summary>
        public string PresentIllness { get; set; }
        /// <summary>
        /// 既往病史
        /// </summary>
        public string PastHistory { get; set; }
        /// <summary>
        /// 个人史及家族史
        /// </summary>
        public string PersonalHistory { get; set; }
        /// <summary>
        /// 过敏史
        /// </summary>
        public string Allergen { get; set; }
        /// <summary>
        /// 既往用药史
        /// </summary>
        public string PastDrugHistory { get; set; }
        #endregion

        #region 伴发疾病与用药情况
        /// <summary>
        /// 伴发疾病与用药情况
        /// </summary>
        public string MedicationSituation { get; set; }
        #endregion

        #region 药物不良反应及处置史
        /// <summary>
        /// 药物不良反应及处置史
        /// </summary>
        public string AdverseDrugReactions { get; set; }
        #endregion

        #region 诊断
        /// <summary>
        /// 入院诊断
        /// </summary>
        public string In_DiagNosis { get; set; }
        /// <summary>
        /// 出院诊断
        /// </summary>
        public string Out_DiagNosis { get; set; }
        #endregion

        #region 检验结果
        /// <summary>
        /// 检验结果
        /// </summary>
        public IList<string> LisLab { get; set; }

        /// <summary>
        /// 病原学检测及药敏
        /// </summary>
        public IList<YaoMinLab> YaoMinWswLab { get; set; }
        #endregion

        #region 检查结果
        /// <summary>
        /// 检查结果
        /// </summary>
        public IList<LisExam> LisExam { get; set; }
        #endregion

        #region 体温单
        /// <summary>
        /// 体温
        /// </summary>
        public IList<Temperature> TemperatureList { get; set; }
        #endregion

        #region 初始化治疗方案分析及药物监护
        /// <summary>
        /// 主要治疗药物
        /// </summary>
        public IList<Drug> InitialDrugDBGrid { get; set; }
        /// <summary>
        /// 初始治疗方案分析
        /// </summary>
        public string InitialProgramme { get; set; }
        /// <summary>
        /// 初始药物治疗监护计划
        /// </summary>
        public string InitialCustody { get; set; }
        #endregion

        #region 其他主要治疗药物
        /// <summary>
        /// 其他药品详细信息
        /// </summary>
        public IList<Drug> OtherTDrugDBGrid { get; set; }
        /// <summary>
        /// 其他主要治疗药物分析
        /// </summary>
        public string OtherMainDrugSummary { get; set; }
        #endregion

        #region 药物治疗日志
        /// <summary>
        /// 治疗药物日志
        /// </summary>
        public IList<TreatmentLog> TreatmentLog = new List<TreatmentLog>();
        #endregion

        #region 药物治疗总结
        /// <summary>
        /// 药物治疗总结
        /// </summary>
        public string SummaryOfDrugTherapy { get; set; }
        #endregion
    }

    public class PatBaseInfo
    {
        #region 基本信息
        /// <summary>
        /// 住院号
        /// </summary>
        public string HospitalNo { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string PatientId { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// 住院次数
        /// </summary>
        public string VisitId { get; set; }
        /// <summary>
        /// 药历号
        /// </summary>
        public string MedNo { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string BirthPlace { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string BirthDate { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public string Height { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 病房床号
        /// </summary>
        public string BedNo { get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 药师
        /// </summary>
        public string Pharmacist { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string Workaddress { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string ContactAddress { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string TelepHone { get; set; }
        /// <summary>
        /// 血型
        /// </summary>
        public string BloodType { get; set; }
        /// <summary>
        /// 血压
        /// </summary>
        public string BloodPressure { get; set; }
        /// <summary>
        /// 体重指数
        /// </summary>
        public string BMI { get; set; }
        /// <summary>
        /// 体表面积
        /// </summary>
        public string BSA { get; set; }
        /// <summary>
        /// 不良嗜好
        /// </summary>
        public string Blsh { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 医生
        /// </summary>
        public string DoctorName { get; set; }
        #endregion
    }
    /// <summary>
    /// 常规检验-检验结果
    /// </summary>
    public class LisLab
    {
        /// <summary>
        /// 检验结果表头let header = ['labcode','labname','itemcode','itemname','unit','20130501','20130520','20130502','20130503']
        /// </summary>
        public IList<string> header { get; set; }
        /// <summary>
        /// 检验结果每一条数据序列化成字符串
        /// </summary>
        public IList<string> labItem { get; set; }
    }


    /// <summary>
    /// 病原学检测及药敏-检验结果
    /// </summary>
    public class YaoMinLab
    {
        public string iid { get; set; }
        public string labcode { get; set; }
        public string labname { get; set; }
        public string sampletype { get; set; }
        public string itemcode { get; set; }
        public string itemname { get; set; }
        public string range { get; set; }
        public string btacode { get; set; }
        public string btaname { get; set; }
        public string labresult { get; set; }
        public string unit { get; set; }
        public string retflagname { get; set; }
        public string samplingtime { get; set; }
        public string requesttime { get; set; }
        public string dels { get; set; }
    }

    /// <summary>
    /// 检查结果
    /// </summary>
    public class LisExam
    {
        public string iid { get; set; }
        public string reportdate { get; set; }
        public string examname { get; set; }
        public string bodypart { get; set; }
        public string examresult { get; set; }
        public string dels { get; set; }
    }

    /// <summary>
    /// 体温单
    /// </summary>
    public class Temperature
    {
        public string taketime { get; set; }
        public string temperature { get; set; }
        public string tempval { get; set; }
    }

    /// <summary>
    /// 初始化药物
    /// </summary>
    public class Drug
    {
        public string cid { get; set; }
        public string grouptag { get; set; }
        public string groupstate { get; set; }
        public string vgroupstate { get; set; }
        public string bgroupstate { get; set; }
        public string is_temp { get; set; }
        public string drugname { get; set; }
        public string dose { get; set; }
        public string route { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string sort { get; set; }
        public string dels { get; set; }
    }

    /// <summary>
    /// 治疗日志
    /// </summary>
    public class TreatmentLog
    {
        /// <summary>
        /// 填写日期
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 新开用药
        /// </summary>
        public IList<LogDrug> drugnew { get; set; }
        /// <summary>
        /// 停止用
        /// </summary>
        public IList<LogDrug> drugStop { get; set; }
        /// <summary>
        /// 标记是否填写日志
        /// </summary>
        public string Mark { get; set; }
        /// <summary>
        /// 是否有新开药品
        /// </summary>
        public string new_mark { get; set; }
        /// <summary>
        /// 是否有停嘱药品
        /// </summary>
        public string stop_mark { get; set; }
        /// <summary>
        /// 药程记录分析
        /// </summary>
        public string DrugHistory { get; set; }
    }

    /// <summary>
    /// 药物治疗日志-药品
    /// </summary>
    public class LogDrug
    {
        public string cid { get; set; }
        public string DrugStatus { get; set; }
        public string DrugName { get; set; }
        public string DrugForm { get; set; }
        public string Dose { get; set; }
        public string PA_EndDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string dels { get; set; }
    }
}
