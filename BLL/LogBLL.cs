using DAL;

namespace BLL
{
    public class LogBLL
    {
        public static void AddLog(int ProcessType, string TableName, int ProcessID)
        {
            LogDAO.AddLog(ProcessType, TableName, ProcessID);
        }
    }
}