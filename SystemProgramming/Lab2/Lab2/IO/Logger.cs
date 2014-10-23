using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.IO
{
    static class Logger
    {
        static StringBuilder builder = new StringBuilder();

        public delegate void LogMessageHandler(string message);

        public static event LogMessageHandler NewStringEvent;

        public static void Clear()
        {
            builder.Clear();
        }

        public static void AppendLine(string str)
        {
            builder.AppendLine(str);
            OnNewMessage(str);
        }

        public static string GetLog()
        {
            return builder.ToString();
        }

        private static void OnNewMessage(string str)
        {
            if(NewStringEvent != null)
            {
                NewStringEvent(str);
            }
        }
                
    }
}
