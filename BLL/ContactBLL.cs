using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class ContactBLL
    {
        ContactDAO dao = new ContactDAO();

        public bool AddContact(GeneralDTO model)
        {
            Contact contact = new Contact();
            contact.NameSurname = model.Name;
            contact.Email = model.Email;
            contact.Subject = model.Subject;
            contact.Message = model.Message;
            contact.AddDate = System.DateTime.Now;
            contact.LastUpdateDate = System.DateTime.Now;
            dao.AddContact(contact);
            return true;
        }

        public List<ContactDTO> GetUnreadMessages()
        {
            return dao.GetUnreadMessages();
        }

        public void ReadMessage(int id)
        {
            dao.ReadMessage(id);
            LogDAO.AddLog(General.ProcessType.ContactRead, General.TableName.Contact, id);
        }

        public void DeleteMessage(int id)
        {
            dao.DeleteMessage(id);
            LogDAO.AddLog(General.ProcessType.ContactDelete, General.TableName.Contact, id);
        }

        public List<ContactDTO> GetAllMessages()
        {
            return dao.GetAllMessages();
        }
    }
}