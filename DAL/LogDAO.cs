using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using DTO;

namespace DAL
{
    public class LogDAO
    {
        public static void AddLog(int ProcessType, string TableName, int ProcessID, SessionDTO session)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Log_Table log = new Log_Table();
                log.UserID = session.UserID;
                log.ProcessType = ProcessType;
                log.ProcessID = ProcessID;
                log.ProcessCategoryType = TableName;
                log.ProcessDate = DateTime.Now;
                log.IPAddress = HttpContext.Current.Request.UserHostAddress;
                db.Log_Table.Add(log);
                db.SaveChanges();
            }
        }

        public List<LogDTO> GetLogs()
        {
            List<LogDTO> dtolist = new List<LogDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                var list = (from l in db.Log_Table
                    join u in db.T_User on l.UserID equals u.ID
                    join p in db.ProcessTypes on l.ProcessType equals p.ID
                    select new
                    {
                        ID = l.ID,
                        UserName = u.Username,
                        ipAddress = l.IPAddress,
                        TableName = l.ProcessCategoryType,
                        TableID = l.ProcessID,
                        ProcessName = p.ProcessName,
                        ProcessDate = l.ProcessDate
                    }).ToList();
                foreach (var item in list)
                {
                    LogDTO dto = new LogDTO();
                    dto.ID = item.ID;
                    dto.UserName = item.UserName;
                    dto.IpAddress = item.ipAddress;
                    dto.TableName = item.TableName;
                    dto.TableID = item.TableID;
                    dto.ProcessName = item.ProcessName;
                    dto.ProcessDate = item.ProcessDate;
                    dtolist.Add(dto);
                }
            }

            return dtolist;
        }
    }
}