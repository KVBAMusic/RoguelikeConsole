using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.UI
{
    public class ActionLogger
    {

        string[] logs;

        public ActionLogger()
        {
            logs = new string[5];
        }

        public void Log(string msg)
        {
            for (int i = 0; i < logs.Length; i++)
            {
                if (logs[i] == "")
                {
                    logs[i] = msg;
                    return;
                }
            }
            logs[4] = msg;
        }

        public void Display()
        {
            for (int i = 0; i < logs.Length; i++)
            {
                Console.WriteLine(logs[i]);
            }    
        }
    }
}
