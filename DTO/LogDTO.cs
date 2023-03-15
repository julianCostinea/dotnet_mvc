using System;

namespace DTO
{
    public class LogDTO
    {
        public int ID { get; set; }
        public string UserName { get; set; }    
        public string IpAddress { get; set; }
        public string TableName { get; set; }
        public int TableID { get; set; }
        public string ProcessName { get; set; }
        public DateTime ProcessDate { get; set; }
    }
}