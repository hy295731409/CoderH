using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices; //包含DllImport的using指令 
using AccessWinform;

namespace AccessWinform
{
    #region 输出结构
    public struct MIS_ICBC_Output
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] //这里的2就是数组长度
        public Byte[] TransType;    //交易指令

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19)]
        public Byte[] CardNo;        //交易卡号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Byte[] Amount;        //交易金额

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Byte[] TipAmount;            //小费金额（暂时不用，空格补齐）

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public Byte[] TransTime;            //交易日期

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public Byte[] TransDate;            //交易日期

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public Byte[] ExpDate;            //卡片有效期

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public Byte[] ReferNo;            //系统检索号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public Byte[] AuthNo;            //授权号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] RspCode;            //返回码

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public Byte[] TerminalId;            //交易终端号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Byte[] MerchantId;            //交易商户号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public Byte[] YLMerchantId;            //银联商户号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] InstallmentTimes;            //分期期数

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public Byte[] TCData;            //IC 卡数据

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public Byte[] MerchantNameEng;            //英文商户名称

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public Byte[] MerchantNameChs;            //中文商户名称

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public Byte[] TerminalTraceNo;            //终端流水号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public Byte[] IcCardId;            //IC 卡序列号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public Byte[] BankName;            //发卡行名称

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public Byte[] TransName;            //中文交易名称

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public Byte[] CardType;            //卡类别

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] TotalInfo;            //交易汇总信息，打印总账时需要

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] RspMessage;            //交易失败时，MISPOS 系统返回中文错误描述信息

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] Remark;            //备注信息

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] WTrace;            //外卡流水号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] AIDDAT;            //AID(IC 卡数据项)

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] APPLBL;            //APPLABEL(IC 卡数据项)

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] APPNAM;            //APPNAME(IC 卡数据项)

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] ElecTotal;            //脱机交易汇总信息

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] SettleAmount;            //实扣金额

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] QROrderNo;            //二维码订单号

        /*二维码优惠支付信息:( 积分抵扣 12+电子券抵扣金额12+优惠券抵扣金额 12+银行立减 12+商户立减 12+订单号 30 该字段全为可视字符，另外目前工行对该字段的设计是长度可变，该字段可能为空，也可能只有一个订单号，后面的金额个数也是未定的，有可能一个金额，也可能多个金额，具体以工行实际返回为准，收银系统这边要对这块做好解析扩展空间)*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public Byte[] QRMemo;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public Byte[] RefNum;            //23 位系统检索号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public Byte[] Channel;            //渠道标识 1-工银二维码、2-银联二维码、3-微信支付、4-支付宝支付

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public Byte[] platId;            //收银机号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public Byte[] operId;            //操作员号


        //char TransType[2]; //交易指令
        //char CardNo[19]; //交易卡号
        //char Amount[12]; //交易金额
        //char TipAmount[12]; //小费金额
        //char TransTime[6]; //交易时间
        //char TransDate[8]; //交易日期
        //char ExpDate[4]; //卡片有效期
        //char ReferNo[8]; //系统检索号
        //char AuthNo[6]; //授权号
        //char RspCode[2]; //返回码
        //char TerminalId[15]; //交易终端号
        //char MerchantId[12]; //交易商户号
        //char YLMerchantId[15]; //银联商户号
        //char InstallmentTimes[2]; //分期期数
        //char TCData[256]; //IC 卡数据
        //char MerchantNameEng[50]; //英文商户名称
        //char MerchantNameChs[40]; //中文商户名称
        //char TerminalTraceNo[6]; //终端流水号
        //char IcCardId[4]; //IC 卡序列号
        //char BankName[40]; //发卡行名称
        //char TransName[20]; //中文交易名称
        //char CardType[20]; //卡类别
        //char TotalInfo[800]; //交易汇总信息，打印总账时需要
        //char RspMessage[100]; //交易失败时，MISPOS 系统返回中文错误描述信息
        //char Remark[300]; //备注信息
        //char WTrace[24]; //外卡流水号
        //char AIDDAT[34]; //AID(IC 卡数据项)
        //char APPLBL[20]; //APPLABEL(IC 卡数据项)
        //char APPNAM[20]; //APPNAME(IC 卡数据项)
        //char ElecTotal[32]; //脱机交易汇总信息
        //char SettleAmount[12];//实扣金额
        //char QROrderNo[50]; //二维码订单号
        //char QRMemo[300]; /*二维码优惠支付信息:( 积分抵扣 12+电子券抵扣金额12+优惠券抵扣金额 12+银行立减 12+商户立减 12+订单号 30 该字段全为可视字符，另外目前工行对该字段的设计是长度可变，该字段可能为空，也可能只有一个订单号，后面的金额个数也是未定的，有可能一个金额，也可能多个金额，具体以工行实际返回为准，收银系统这边要对这块做好解析扩展空间)*/
        //char RefNum[23]; //23 位系统检索号
        //char Channel[1]; //渠道标识 1-工银二维码、2-银联二维码、3-微信支付、4-支付宝支付
        //char platId[20]; //收银机号
        //char operId[20]; //操作员号
    }

    #endregion

    #region 输入结构
    public struct MIS_ICBC_Input
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] //这里的2就是数组长度
        public Byte[] TransType;    //交易指令

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public Byte[] FuncID;        //分行特色脚本 ID 号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Byte[] TransAmount;        //交易金额

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Byte[] TipAmount;            //小费金额（暂时不用，空格补齐）

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public Byte[] TransDate;            //交易日期

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public Byte[] MisTraceNo;            //MIS 流水号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19)]
        public Byte[] CardNo;            //交易卡号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public Byte[] ExpDate;            //卡片有效期

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public Byte[] ReferNo;            //系统检索号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public Byte[] AuthNo;            //授权号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Byte[] MultiId;            //多商户交易索引号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public Byte[] TerminalId;            //交易终端号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public Byte[] InstallmentTimes;            //分期期数

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public Byte[] PreInput;            //预输入项 附件说明 1

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public Byte[] AddDatas;            //固定输入项 附件说明 2

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public Byte[] QRCardNO;            //二维码支付号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public Byte[] QROrderNo;            //二维码订单号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public Byte[] PrnFeatureData;            //特色打印数据 附件说明 4

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
        public Byte[] BillInfo;            ////附加在对账单上的信息。如商户可传入自己系统的交易流水号，在银行提供对账单上每一笔交易将会附加该流水号，方便查阅对账。

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public Byte[] platId;            //收银机号

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public Byte[] operId;            //操作员号

        //c++枚举
        //char TransType[2]; //交易指令
        //char FuncID[4]; //分行特色脚本 ID 号
        //char TransAmount[12]; //交易金额
        //char TipAmount[12]; //小费金额
        //char TransDate[8]; //交易日期
        //char MisTraceNo[6];//MIS 流水号
        //char CardNo[19]; //交易卡号
        //char ExpDate[4]; //卡片有效期
        //char ReferNo[8]; //系统检索号
        //char AuthNo[6]; //授权号
        //char MultiId[12]; //多商户交易索引号
        //char TerminalId[15]; //交易终端号
        //char InstallmentTimes[2]; //分期期数
        //char PreInput[256]; //预输入项 附件说明 1
        //char AddDatas[256]; //固定输入项 附件说明 2
        //char QRCardNO[50]; //二维码支付号
        //char QROrderNo[50]; //二维码订单号
        //char PrnFeatureData[512]; //特色打印数据 附件说明 4
        //char BillInfo[60]; //附加在对账单上的信息。如商户可传入自己系统的交易流水号，在银行提供对账单上每一笔交易将会附加该流水号，方便查阅对账。
        //char platId[20]; //收银机号
        //char operId[20]; //操作员号
    }
    #endregion

    public class LoadDLLTest
    {



        #region 声明动态载入DLL的参数
        //static MIS_ICBC_Output output = new MIS_ICBC_Output();
        //static MIS_ICBC_Input input = new MIS_ICBC_Input();

        //string path = System.AppDomain.CurrentDomain.BaseDirectory;

        //public LoadDLLTest(MIS_ICBC_Output output, MIS_ICBC_Output input)
        //{
        //    this.output = output;
        //    this.output = output;
        //}
        #endregion

        [DllImport("KeeperClient.dll", CallingConvention = CallingConvention.StdCall)]
        static extern int misposTrans(IntPtr input, IntPtr output);//加载DLL
        public static void ReadDll()
        {
            try
            {

                MIS_ICBC_Input i = new MIS_ICBC_Input();
                IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(i));
                Marshal.StructureToPtr(i, ptr, false);

                var o = new MIS_ICBC_Output();
                IntPtr pout = Marshal.AllocHGlobal(Marshal.SizeOf(o));
                Marshal.StructureToPtr(o, pout, false);


                var res = misposTrans(ptr, pout);



                //LuckyDiscovery(input, out output);
                //string pathDLL = System.AppDomain.CurrentDomain.BaseDirectory + "KeeperClient\\KeeperClient.dll";
                //string pathDLL = @"../KeeperClient\KeeperClient.DLL";
                //1、利用反射进行动态加载和调用.
                Assembly assembly = Assembly.LoadFrom(System.AppDomain.CurrentDomain.BaseDirectory);  //利用dll的路径加载,同时将此程序集所依赖的程序集加载进来,需后辍名.dll
                Assembly.LoadFile("KeeperClient.DLL");

                //2、加载dll后,需要使用dll中某类.
                Type type = assembly.GetType("KeeperClient");//用类型的命名空间和名称获得类型

                //3、需要实例化类型,才可以使用,参数可以人为的指定,也可以无参数,静态实例可以省略
                Object obj = Activator.CreateInstance(type);//利用指定的参数实例话类型

                //4、调用类型中的某个方法:
                //需要首先得到此方法
                MethodInfo mi = type.GetMethod("MehtodName");//通过方法名称获得方法

                //5、然后对方法进行调用,多态性利用参数进行控制
                //mi.Invoke(obj);//根据参数直线方法,返回值就是原方法的返回值
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void LoadDLL2nd()
        {

        }
    }
}
