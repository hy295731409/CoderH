using log4net;
using Medicom.Common.DatabaseDriver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace Medicom.PASSPA2CollectService
{
    /// <summary>
    /// 解析消息
    /// </summary>
    public class Analysis
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Analysis));
        private Message entity;
        public Analysis(string message)
        {
            entity = Helper.ReturnEntity(message);
        }
        public void Action()
        {
            try
            {
                var MeassgeType = entity.MessageType;
                //switch (MeassgeType)
                //{
                //    //代表字典消息
                //    case Constant.MFN_M01:
                //        if (!string.IsNullOrEmpty(entity.Content.Find(e => e.Contains("Z01"))))//科室
                //            new DictDept().Run(entity);
                //        else if (!string.IsNullOrEmpty(entity.Content.Find(e => e.Contains("Z02"))))//人员
                //            new DictDoctor().Run(entity);
                //        else if (!string.IsNullOrEmpty(entity.Content.Find(e => e.Contains("Z03"))))//病区
                //            new DictWard().Run(entity);
                //        else if (!string.IsNullOrEmpty(entity.Content.Find(e => e.Contains("Z06"))))//药品
                //            new DictDrug().Run(entity);
                //        else if (!string.IsNullOrEmpty(entity.Content.Find(e => e.Contains("ZA3"))))//频率
                //            new DictFrequency().Run(entity);
                //        else if (!string.IsNullOrEmpty(entity.Content.Find(e => e.Contains("Z04"))))//检验检查
                //            new DictLab().Run(entity);
                //        else if (!string.IsNullOrEmpty(entity.Content.Find(e => e.Contains("ZA2"))))//途径
                //            new DictRoute().Run(entity);
                //        else if (!string.IsNullOrEmpty(entity.Content.Find(e => e.Contains("ZD1"))))//手术(his平台不会推送)
                //            new DictOperation().Run(entity);
                //        else if (!string.IsNullOrEmpty(entity.Content.Find(e => e.Contains("Z10"))))//疾病
                //            new DictDisease().Run(entity);
                //        break;
                //    //门诊挂号
                //    case Constant.ADT_A04:
                //        new ClinicPatient().Run(entity);
                //        break;
                //    //住院登记/婴儿登记
                //    case Constant.ADT_A01:
                //        new HospPatient().Run(entity);
                //        break;
                //    //修改患者信息
                //    case Constant.ADT_A08:
                //        new ClinicPatient().Run(entity);
                //        break;
                //    //诊断信息(his平台不会推送)
                //    case Constant.ADT_A31:
                //        new ClinicDisease().Run(entity);
                //        break;
                //    //检查信息
                //    case Constant.ORU_R01:
                //        new ClinicExam().Run(entity);
                //        break;
                //    //检验信息
                //    case Constant.OUL_R21:
                //        new ClinicLab().Run(entity);
                //        break;
                //    //处方和医嘱信息
                //    case Constant.OMP_O09:
                //        new ClinicOrder().Run(entity);
                //        new ClinicPresc().Run(entity);
                //        break;
                //    //门诊处方收费信息(his平台不会推送)
                //    case Constant.ORP_O10:
                //        new ClinicCost().Run(entity);
                //        break;
                //    //住院医嘱费用
                //    case Constant.RAS_O17:
                //        new HospCost().Run(entity);
                //        break;
                //    //门诊/住院检查收费
                //    case Constant.ORG_O20:
                //        new ClinicCost().Run(entity);
                //        break;
                //    //门诊/住院检验收费
                //    case Constant.ORL_O22:
                //        new ClinicCost().Run(entity);
                //        break;
                //    //住院转科
                //    case Constant.ADT_A02:
                //        new HospTransferred().Run(entity);
                //        break;
                //    //换床
                //    case Constant.ADT_A42:
                //        new HospPatient().Run(entity);
                //        break;
                //    //更换主管医生
                //    case Constant.ADT_A54:
                //        new HospPatient().Run(entity);
                //        break;
                //    //出院结算
                //    case Constant.ADT_A03:
                //        new ClinicPatient().Run(entity);
                //        break;
                //}
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("消息{0}处理失败：{1}", entity.MessageId, ex.Message);
                logger.Error("Source：" + ex.Source);
                logger.Error("TargetSite：" + ex.TargetSite);
            }
        }
    }
}
