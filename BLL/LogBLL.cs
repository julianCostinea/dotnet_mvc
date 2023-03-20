using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class LogBLL
    {
        public static void AddLog(int ProcessType, string TableName, int ProcessID, SessionDTO session)
        {
            LogDAO.AddLog(ProcessType, TableName, ProcessID, session);
        }
        LogDAO dao = new LogDAO();
        public List<LogDTO> GetLogs()
        {
            return dao.GetLogs();
        }
    }
}