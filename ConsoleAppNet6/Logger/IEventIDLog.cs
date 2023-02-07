using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet6.Logger
{
    public interface IEventIDLog : ILog
    {
		void Info(int eventId, object message);
		void Info(int eventId, object message, Exception t);

		void Warn(int eventId, object message);
		void Warn(int eventId, object message, Exception t);

		void Error(int eventId, object message);
		void Error(int eventId, object message, Exception t);

		void Fatal(int eventId, object message);
		void Fatal(int eventId, object message, Exception t);
	}
}
