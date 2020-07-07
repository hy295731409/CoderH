using ConsoleApp4._7.TaskDemo;
using ConsoleApp4._7.test;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4._7
{
    class Program
    {
        static void Main(string[] args)
        {
            //TaskClass.GetTestRes();
            //Console.WriteLine("flag");


            //Person person1 = new Person();

            //string json1 = JsonConvert.SerializeObject(person1, Formatting.Indented);
            //string json = JsonConvert.SerializeObject(person1);
            //Console.WriteLine("--------包含属性的默认值与null序列化-------");
            //Console.WriteLine(json1);


            //Console.WriteLine("--------不包含属性的默认值序列化-------");

            //Person person2 = new Person()
            //{
            //    Name = "GongHui",
            //    Age = 28
            //};

            //string json2 = JsonConvert.SerializeObject(person2, Formatting.Indented, new JsonSerializerSettings
            //{
            //    DefaultValueHandling = DefaultValueHandling.Ignore
            //});
            //Console.WriteLine(json2);

            //Console.WriteLine("--------不包含属性的null序列化-------");

            //string json3 = JsonConvert.SerializeObject(person2, Formatting.Indented, new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore
            //});
            //Console.WriteLine(json3);


            //var s = "{\"Name\":null,\"Age\":0,\"Partner\":null,\"Salary\":0.0}";
            //var p1 = JsonConvert.DeserializeObject<Person>(s);
            //s = "{\"Name\":null,\"Age\":0,\"Partner\":null}";
            //var p2 = JsonConvert.DeserializeObject<Person>(s);

            #region 电子药历序列化
            var en = new MedRecord();
            var baseInfo = new PatBaseInfo();

            //基本信息
            baseInfo.HospitalNo = "0000360805";
            baseInfo.PatientId = "ZyZY010000360805_1";
            baseInfo.PatientName = "田丕义";
            baseInfo.VisitId = "1";
            baseInfo.MedNo = "0000360805_20200622";
            baseInfo.BirthPlace = "";
            baseInfo.Nation = "";
            baseInfo.BirthDate = "1969-05-06";
            baseInfo.Sex = "男";
            baseInfo.Age = "45";
            baseInfo.Height = "173";
            baseInfo.Weight = "85";
            baseInfo.BedNo = "312113-2";
            baseInfo.StartDate = "2014-05-04";
            baseInfo.EndDate = "2014-07-22";
            baseInfo.Pharmacist = "黄小平";
            baseInfo.Workaddress = "电信";
            baseInfo.ContactAddress = "成都嘀嘀嘀";
            baseInfo.TelepHone = "13104177286";
            baseInfo.BloodType = "a";
            baseInfo.BloodPressure = "170mmhg";
            baseInfo.BMI = "28.401";
            baseInfo.BSA = "1.99";
            baseInfo.Blsh = "烟酒";
            baseInfo.DeptName = "放疗一";
            baseInfo.DoctorName = "丁谦钰";
            en.BaseInfo = baseInfo;

            //病史及用药史
            en.AdvancedAge = 0;
            en.LiverFunction = 0;
            en.RenalFunction = 0;
            en.MainSuit = "主诉";
            en.PresentIllness = "现病史";
            en.PastHistory = "既往病史";
            en.PersonalHistory = "家族史";
            en.Allergen = "过敏史";
            en.PastDrugHistory = "既往用药史";

            //伴发疾病与用药情况
            en.MedicationSituation = "伴发疾病与用药情况";

            //药物不良反应及处置史
            en.AdverseDrugReactions = "药物不良反应及处置史";

            //诊断
            en.In_DiagNosis = "肺恶性肿瘤";
            en.Out_DiagNosis = "肺恶性肿瘤(好转),腺癌NOS";

            //检验结果
            var strArr = new[] 
            { 
                "{\"20130501\":\"73 ↑, 79 ↑\",\"20130502\":\"23, 25\",\"20130503\":\"33\",\"20130520\":\"51 ↑\",\"labresultcode\":\"ALT谷丙转氨酶0～40U / L\",\"labcode\":\"ALT谷丙转氨酶0～40U / L\",\"itemcode\":\"ALT\",\"labname\":\"滴虫培养, 血常规\",\"itemname\":\"谷丙转氨酶\",\"rangeunit\":\"0～40\",\"unit\":\"U/L\"}" ,
                "{\"20130501\":\"72 ↑, 76 ↑\",\"20130502\":\"22, 22\",\"20130503\":\"30\",\"20130520\":\"55 ↑\",\"labresultcode\":\"ALT谷丙转氨酶0～40U / L\",\"labcode\":\"ALT谷丙转氨酶0～40U / L\",\"itemcode\":\"ALT\",\"labname\":\"滴虫培养, 血常规\",\"itemname\":\"谷丙转氨酶\",\"rangeunit\":\"0～40\",\"unit\":\"U/L\"}" ,
            };

            en.LisLab = strArr;
            en.YaoMinWswLab = new List<YaoMinLab>
            {
                new YaoMinLab()
                {
                    iid = "35",
                    labcode = "lab001",
                    labname = "细菌培养(60元)",
                    sampletype = "分泌物",
                    itemcode = "TPXD",
                    itemname = "头孢西丁",
                    btacode = "",
                    btaname = "",
                    labresult = ">0.25",
                    unit = "cfu/mL",
                    retflagname = "耐药",
                    samplingtime = "2013-11-01 08:03:00",
                    requesttime = "",
                    range = "0-0.5"
                },
                new YaoMinLab()
                {
                    iid = "36",
                    labcode = "lab002",
                    labname = "细菌培养(61元)",
                    sampletype = "分泌物2",
                    itemcode = "TPXD2",
                    itemname = "头孢西丁2",
                    btacode = "",
                    btaname = "",
                    labresult = ">0.26",
                    unit = "cfu/mL",
                    retflagname = "耐药",
                    samplingtime = "2013-11-02 08:03:00",
                    requesttime = "",
                    range = "0-0.5"
                },
            };


            //检查
            en.LisExam = new List<LisExam>
            {
                new LisExam()
                {
                    iid = "15",
                    reportdate = "2013-11-03",
                    examname = "磁共振平扫",
                    examresult = "胸部未见异常",
                    bodypart = "胸部"
                },
                new LisExam()
                {
                    iid = "16",
                    reportdate = "2013-11-04",
                    examname = "磁共振平扫",
                    examresult = "胸部未见异常",
                    bodypart = "胸部"
                },
            };

            //体温
            en.TemperatureList = new List<Temperature>
            {
                new Temperature
                {
                    temperature = "37.8",
                    tempval = "37.8",
                    taketime = "2014-07-22 00:00:00"
                },
                new Temperature
                {
                    temperature = "36.8",
                    tempval = "36.8",
                    taketime = "2014-07-20 00:00:00"
                },
            };

            //初始化治疗方案分析及药物监护
            en.InitialDrugDBGrid = new List<Drug>
            {
                new Drug
                {
                    cid = "cy-004cpz0",
                    grouptag = "1",
                    groupstate = "1",
                    vgroupstate = "┏",
                    bgroupstate = "1",
                    is_temp = "长期",
                    drugname = "哌拉西林他唑巴坦针(华北1.25)",
                    dose = "1.00克",
                    route = "滴眼 BID8",
                    startdate = "2013-11-01 08:13:00",
                    enddate = "2013-11-01 18:13:00",
                    sort = "2013-11-01012013-11-01 08:13:00cy-004cpz0",
                    dels = "删除"
                }
            };
            en.InitialProgramme = "初始治疗方案分析";
            en.InitialCustody = "初始药物治疗监护计划";

            //其他主要治疗药物
            en.OtherTDrugDBGrid = new List<Drug>
            {
                new Drug
                {
                    cid = "cy-004cpz113",
                    grouptag = "1",
                    groupstate = "3",
                    vgroupstate = "┣",
                    bgroupstate = "3",
                    is_temp = "长期",
                    drugname = "头孢唑林",
                    dose = "1.00毫克",
                    route = "ivgtt BID8",
                    startdate = "2013-11-02 08:13:00",
                    enddate = "2013-11-07 18:13:00",
                    sort = "2013-11-02012013-11-02 08:13:00cy-004cpz113",
                    dels = "删除"
                }
            };
            en.OtherMainDrugSummary = "其他主要治疗药物分析";

            //药物治疗日志
             var log1 = new TreatmentLog
                {
                    date = "2014-05-04",
                    DrugHistory = "药程记录分析",
                    Mark = "1",
                    new_mark = "1",
                    stop_mark = "1",
                    drugnew = new List<LogDrug>()
                    {
                        new LogDrug
                        {
                            cid = "cy-004cpz0",
                            DrugStatus = "长期",
                            DrugName = "哌拉西林他唑巴坦针(华北1.25)",
                            DrugForm = "粉针剂",
                            PA_EndDate = "",
                            EndDate = "2013-11-01 18:13:00",
                            dels = "删除",
                            Dose = "1克 滴眼 BID8",
                            StartDate = "2013-11-01 08:13:00"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz2",
                            DrugStatus = "长期",
                            DrugName = "注射用盐酸托烷司琼圣吉",
                            DrugForm = "注射剂",
                            PA_EndDate = "",
                            EndDate = "2014-07-18 08:09:19",
                            dels = "删除",
                            Dose = "5mg iv.dri QD",
                            StartDate = "2014-07-18 23:09:19"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz3",
                            DrugStatus = "长期",
                            DrugName = "0.9%氯化钠注射液",
                            DrugForm = "注射液",
                            PA_EndDate = "",
                            EndDate = "2014-07-18 08:09:19",
                            dels = "删除",
                            Dose = "100ml iv.dri QD",
                            StartDate = "2014-07-18 23:09:19"
                        }
                    },
                    drugStop = new List<LogDrug>()
                    {
                        new LogDrug
                        {
                            cid = "cy-004cpz115",
                            DrugStatus = "长期",
                            DrugName = "阿莫西林钠/舒巴坦钠(来切利针)",
                            DrugForm = "针剂",
                            PA_EndDate = "",
                            EndDate = "2013-11-01 18:13:00",
                            dels = "删除",
                            Dose = "1克 ivgtt BID8",
                            StartDate = "2013-11-01 08:13:00"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz115",
                            DrugStatus = "长期",
                            DrugName = "注射用胸腺五肽",
                            DrugForm = "注射剂",
                            PA_EndDate = "",
                            EndDate = "14-07-14 08:44:01",
                            dels = "删除",
                            Dose = "20mg iv.dri QD",
                            StartDate = "14-07-14 23:44:01"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz115",
                            DrugStatus = "长期",
                            DrugName = "顺铂注射液",
                            DrugForm = "注射液",
                            PA_EndDate = "",
                            EndDate = "2014-07-14 08:47:19",
                            dels = "删除",
                            Dose = "30mg iv.dri QD",
                            StartDate = "2014-07-14 23:47:19"
                        }
                    },

                };
            var log2 = new TreatmentLog
            {
                date = "2014-05-14",
                DrugHistory = "药程记录分析",
                Mark = "1",
                new_mark = "1",
                stop_mark = "1",
                drugnew = new List<LogDrug>()
                    {
                        new LogDrug
                        {
                            cid = "cy-004cpz0",
                            DrugStatus = "长期",
                            DrugName = "哌拉西林他唑巴坦针(华北1.25)",
                            DrugForm = "粉针剂",
                            PA_EndDate = "",
                            EndDate = "2013-11-01 18:13:00",
                            dels = "删除",
                            Dose = "1克 滴眼 BID8",
                            StartDate = "2013-11-01 08:13:00"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz2",
                            DrugStatus = "长期",
                            DrugName = "注射用盐酸托烷司琼圣吉",
                            DrugForm = "注射剂",
                            PA_EndDate = "",
                            EndDate = "2014-07-18 08:09:19",
                            dels = "删除",
                            Dose = "5mg iv.dri QD",
                            StartDate = "2014-07-18 23:09:19"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz3",
                            DrugStatus = "长期",
                            DrugName = "0.9%氯化钠注射液",
                            DrugForm = "注射液",
                            PA_EndDate = "",
                            EndDate = "2014-07-18 08:09:19",
                            dels = "删除",
                            Dose = "100ml iv.dri QD",
                            StartDate = "2014-07-18 23:09:19"
                        }
                    },
                drugStop = new List<LogDrug>()
                    {
                        new LogDrug
                        {
                            cid = "cy-004cpz115",
                            DrugStatus = "长期",
                            DrugName = "阿莫西林钠/舒巴坦钠(来切利针)",
                            DrugForm = "针剂",
                            PA_EndDate = "",
                            EndDate = "2013-11-01 18:13:00",
                            dels = "删除",
                            Dose = "1克 ivgtt BID8",
                            StartDate = "2013-11-01 08:13:00"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz115",
                            DrugStatus = "长期",
                            DrugName = "注射用胸腺五肽",
                            DrugForm = "注射剂",
                            PA_EndDate = "",
                            EndDate = "14-07-14 08:44:01",
                            dels = "删除",
                            Dose = "20mg iv.dri QD",
                            StartDate = "14-07-14 23:44:01"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz115",
                            DrugStatus = "长期",
                            DrugName = "顺铂注射液",
                            DrugForm = "注射液",
                            PA_EndDate = "",
                            EndDate = "2014-07-14 08:47:19",
                            dels = "删除",
                            Dose = "30mg iv.dri QD",
                            StartDate = "2014-07-14 23:47:19"
                        }
                    },

            };
            var log3 = new TreatmentLog
            {
                date = "2014-06-14",
                DrugHistory = "药程记录分析",
                Mark = "1",
                new_mark = "1",
                stop_mark = "1",
                drugnew = new List<LogDrug>()
                    {
                        new LogDrug
                        {
                            cid = "cy-004cpz0",
                            DrugStatus = "长期",
                            DrugName = "哌拉西林他唑巴坦针(华北1.25)",
                            DrugForm = "粉针剂",
                            PA_EndDate = "",
                            EndDate = "2013-11-01 18:13:00",
                            dels = "删除",
                            Dose = "1克 滴眼 BID8",
                            StartDate = "2013-11-01 08:13:00"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz2",
                            DrugStatus = "长期",
                            DrugName = "注射用盐酸托烷司琼圣吉",
                            DrugForm = "注射剂",
                            PA_EndDate = "",
                            EndDate = "2014-07-18 08:09:19",
                            dels = "删除",
                            Dose = "5mg iv.dri QD",
                            StartDate = "2014-07-18 23:09:19"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz3",
                            DrugStatus = "长期",
                            DrugName = "0.9%氯化钠注射液",
                            DrugForm = "注射液",
                            PA_EndDate = "",
                            EndDate = "2014-07-18 08:09:19",
                            dels = "删除",
                            Dose = "100ml iv.dri QD",
                            StartDate = "2014-07-18 23:09:19"
                        }
                    },
                drugStop = new List<LogDrug>()
                    {
                        new LogDrug
                        {
                            cid = "cy-004cpz115",
                            DrugStatus = "长期",
                            DrugName = "阿莫西林钠/舒巴坦钠(来切利针)",
                            DrugForm = "针剂",
                            PA_EndDate = "",
                            EndDate = "2013-11-01 18:13:00",
                            dels = "删除",
                            Dose = "1克 ivgtt BID8",
                            StartDate = "2013-11-01 08:13:00"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz115",
                            DrugStatus = "长期",
                            DrugName = "注射用胸腺五肽",
                            DrugForm = "注射剂",
                            PA_EndDate = "",
                            EndDate = "14-07-14 08:44:01",
                            dels = "删除",
                            Dose = "20mg iv.dri QD",
                            StartDate = "14-07-14 23:44:01"
                        },
                        new LogDrug
                        {
                            cid = "cy-004cpz115",
                            DrugStatus = "长期",
                            DrugName = "顺铂注射液",
                            DrugForm = "注射液",
                            PA_EndDate = "",
                            EndDate = "2014-07-14 08:47:19",
                            dels = "删除",
                            Dose = "30mg iv.dri QD",
                            StartDate = "2014-07-14 23:47:19"
                        }
                    },

            };
            en.TreatmentLog = new List<TreatmentLog>
            {
                log1,log2,log3
            };

            //药物治疗总结
            en.SummaryOfDrugTherapy = "药物治疗总结";
            en.Version = "1.0.202103.1";

            //
            var str = JsonConvert.SerializeObject(en);
            #endregion

            var taskFactory = new TaskFactory();
            Console.WriteLine($"threadId1=" + Thread.CurrentThread.ManagedThreadId);
            taskFactory.StartNew(() => 
            {
                Console.WriteLine($"threadId2=" + Thread.CurrentThread.ManagedThreadId);
                Demo1.GetResAsync("test");
                Console.WriteLine($"threadId3=" + Thread.CurrentThread.ManagedThreadId);
            });
            Console.WriteLine($"threadId4=" + Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
