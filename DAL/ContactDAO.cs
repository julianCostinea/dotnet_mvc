using System.Collections.Generic;
using System.Linq;
using DTO;

namespace DAL
{
    public class ContactDAO:PostContext
    {
        public void AddContact(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
        }

        public List<ContactDTO> GetUnreadMessages()
        {
            List<ContactDTO> dtolist = new List<ContactDTO>();
            var list = db.Contacts.Where(x => x.isRead == false).ToList();
            foreach (var item in list)
            {
                ContactDTO dto = new ContactDTO();
                dto.ID = item.ID;
                dto.Name = item.NameSurname;
                dto.Email = item.Email;
                dto.Subject = item.Subject;
                dto.Message = item.Message;
                dto.AddDate = item.AddDate;
                dto.isRead = item.isRead;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public void ReadMessage(int id)
        {
            Contact contact = db.Contacts.FirstOrDefault(x => x.ID == id);
            contact.isRead = true;
            contact.ReadUserID = UserStatic.UserID;
            contact.LastUpdateDate = System.DateTime.Now;
            contact.LastUpdateUserID = UserStatic.UserID;
            db.SaveChanges();
        }

        public void DeleteMessage(int id)
        {
            Contact contact = db.Contacts.FirstOrDefault(x => x.ID == id);
            contact.isDeleted = true;
            contact.LastUpdateDate = System.DateTime.Now;
            contact.LastUpdateUserID = UserStatic.UserID;
            contact.DeleteDate = System.DateTime.Now;
            db.SaveChanges();
        }

        public List<ContactDTO> GetAllMessages()
        {
            List<ContactDTO> dtolist = new List<ContactDTO>();
            var list = db.Contacts.Where(x => x.isDeleted == false).ToList();
            foreach (var item in list)
            {
                ContactDTO dto = new ContactDTO();
                dto.ID = item.ID;
                dto.Name = item.NameSurname;
                dto.Email = item.Email;
                dto.Subject = item.Subject;
                dto.Message = item.Message;
                dto.AddDate = item.AddDate;
                dto.isRead = item.isRead;
                dtolist.Add(dto);
            }
            return dtolist;
        }
    }
}