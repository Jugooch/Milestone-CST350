using NLog;

namespace Activity2_2.Services.Utilities
{
    public class MyLogger : ILogger
    {
        private static MyLogger instance;
        private static Logger logger;

        public ILogger GetInstance()
        {
            if (instance == null)
            {
                instance = new MyLogger();
            }
            return instance;
        }

        public static Logger GetLogger()
        {
            if (logger == null)
            {
                logger = LogManager.GetLogger("logconsolerule");
            }
            return logger;
        }

        public void Debug(string message)
        {
            GetLogger().Debug(message);
        }

        public void Error(string message)
        {
            GetLogger().Error(message);
        }

        public void Info(string message)
        {
            GetLogger().Info(message);
        }

        public void Warning(string message)
        {
            GetLogger().Warn(message);
        }
    }
}
