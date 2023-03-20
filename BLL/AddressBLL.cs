using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class AddressBLL
    {
        AddressDAO dao = new AddressDAO();
        public bool AddAddress(AddressDTO model, SessionDTO session)
        {
            Address ads = new Address();
            ads.Address1 = model.AddressContent;
            ads.Email = model.Email;
            ads.Phone = model.Phone;
            ads.Phone2 = model.Phone2;
            ads.Fax = model.Fax;
            ads.MapPathLarge = model.LargeMapPath;
            ads.MapPathSmall = model.SmallMapPath;
            ads.AddDate = System.DateTime.Now;
            ads.LastUpdateDate = System.DateTime.Now;
            ads.LastUpdateUserID = session.UserID;
            int ID = dao.AddAddress(ads);
            LogDAO.AddLog(General.ProcessType.AddressAdd, General.TableName.Address, ID, session);
            return true;
        }

        public List<AddressDTO> GetAddresses()
        {
            return dao.GetAddresses();
        }

        public bool UpdateAddress(AddressDTO model, SessionDTO session)
        {
            dao.UpdateAddress(model, session);
            LogDAO.AddLog(General.ProcessType.AddressUpdate, General.TableName.Address, model.ID, session);
            return true;
        }

        public void DeleteAddress(int id, SessionDTO session)
        {
            dao.DeleteAddress(id, session);
            LogDAO.AddLog(General.ProcessType.AddressDelete, General.TableName.Address, id, session);
        }
    }
}