using log4net;
using Medicom.PASSPA2CollectService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Topshelf;

namespace SocketDemo
{
    public class Server : ServiceControl
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Server));
        public bool Start(HostControl hostControl)
        {
            try
            {
                var task = new AcceptMessage();
                new Thread(new ThreadStart(task.Run)).Start();
                new Thread(new ThreadStart(task.TaskBegin)).Start();//创建从队列取数据的线程
            }
            catch (Exception ex)
            {
                logger.Fatal(string.Format("Server start failed: {0}", ex.Message), ex);
            }

            logger.Info("Server started successfully");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            try
            {
                logger.Info("Server stop begin");
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            logger.Info("Server stop complete");
            logger.Info("        * * * * * 服务停止 * * * * *        ");
            return true;
        }
    }
}
