using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class logKaydi
    {
        
            public void gonder(Exception ex)
            {
                string logBilgi = "log.txt";
                string logMessage = $"{DateTime.Now}: {ex.Message} {Environment.NewLine}StackTrace: {ex.StackTrace}{Environment.NewLine}";
                File.AppendAllText(logBilgi, logMessage);
            }
            
        
    }
}
