using NLog;

namespace MyApplication
{
    public class MyLogger : IMyLogger
    {
        readonly Logger logger;

        public MyLogger()
        {
            logger = LogManager.GetLogger("ActionFilter");
        }

        public void Info(string message)
        {
            logger.Info(message);
        }
    }
}