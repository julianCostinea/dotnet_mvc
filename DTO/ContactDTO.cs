namespace DTO
{
    public class ContactDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool isRead { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}