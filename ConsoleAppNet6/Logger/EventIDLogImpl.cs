using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet6.Logger
{
    internal class EventIDLogImpl : LogImpl, IEventIDLog
    {
		/// <summary>
		/// The fully qualified name of this declaring type not the type of any subclass.
		/// </summary>
		private readonly static Type ThisDeclaringType = typeof(EventIDLogImpl);

		public EventIDLogImpl(ILogger logger) : base(logger)
		{
		}

		#region Implementation of IEventIDLog

		public void Info(int eventId, object message)
		{
			Info(eventId, message, null);
		}

		public void Info(int eventId, object message, System.Exception t)
		{
			if (this.IsInfoEnabled)
			{
				LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, message, t);
				loggingEvent.Properties["EventID"] = eventId;
				Logger.Log(loggingEvent);
			}
		}

		public void Warn(int eventId, object message)
		{
			Warn(eventId, message, null);
		}

		public void Warn(int eventId, object message, System.Exception t)
		{
			if (this.IsWarnEnabled)
			{
				LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Warn, message, t);
				loggingEvent.Properties["EventID"] = eventId;
				Logger.Log(loggingEvent);
			}
		}

		public void Error(int eventId, object message)
		{
			Error(eventId, message, null);
		}

		public void Error(int eventId, object message, System.Exception t)
		{
			if (this.IsErrorEnabled)
			{
				LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Error, message, t);
				loggingEvent.Properties["EventID"] = eventId;
				Logger.Log(loggingEvent);
			}
		}

		public void Fatal(int eventId, object message)
		{
			Fatal(eventId, message, null);
		}

		public void Fatal(int eventId, object message, System.Exception t)
		{
			if (this.IsFatalEnabled)
			{
				LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Fatal, message, t);
				loggingEvent.Properties["EventID"] = eventId;
				Logger.Log(loggingEvent);
			}
		}

		#endregion Implementation of IEventIDLog
	}
}
