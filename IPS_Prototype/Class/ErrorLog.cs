using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IPS_Prototype.Class
{
    public class ErrorLog
    {
        public static void WriteErrorLog(string strErrorText)
        {
            try
            {
                //DECLARE THE FILENAME FROM THE ERRORLOG
                string strFilename = "errorLog.txt";

                //DECLARE THE FOLDER WHERE THE LOGFILE HAS TO BE STORED
                string strpath = HttpContext.Current.Request.PhysicalApplicationPath;

                //WRITE THE ERROR TEXT AND THE CURRENT DATE-TIME TO THE ERROR FILE
                System.IO.File.AppendAllText(strpath + "//" + strFilename, strErrorText + "\r\n" + "- " + DateTime.Now.ToString() + "\r\n \r\n");
            }
            catch (Exception ex)
            {
                WriteErrorLog("Error in WriteErrorLog: " + ex.Message);
            }
        }
    }
    
}