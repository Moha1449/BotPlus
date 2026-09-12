using DataModelLayer.ReturnResult;
using System.Collections.Generic;

namespace BusinessLayer.BotEngine
{
    internal static class clsStoppedHandlersLogger
    {
        private static object _Key = new object();
        private static List<clsReturnResult> _Logger = new List<clsReturnResult>();

        internal static  int LogsNumber()
        {
            lock (_Key) 
                return _Logger.Count;   
        }

        internal static List<clsReturnResult> GetLog()
        {
            lock (_Key)
            {
                return _Logger;
            }
        }

        internal static void LogNew(clsReturnResult Details)
        {
            lock (_Key)
                _Logger.Add(Details);
        }
    }
}
