using System;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class UserBLL
    {
        UserDAO userdao = new UserDAO();
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            dto = userdao.GetUserWithUsernameAndPassword(model);
            return dto;
        }

        public void AddUser(UserDTO model, SessionDTO session)
        {
            T_User user = new T_User();
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.ImagePath = model.Imagepath;
            user.NameSurname = model.Name;
            user.isAdmin = model.isAdmin;
            user.isDeleted = false;
            user.AddDate = DateTime.Now;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = session.UserID;
            int ID = userdao.AddUser(user);
            LogDAO.AddLog(General.ProcessType.UserAdd, General.TableName.User, ID, session);
        }

        public List<UserDTO> GetUsers()
        {
            return userdao.GetUsers();
        }

        public UserDTO GetUserWithID(int ID)
        {
            return userdao.GetUserWithID(ID);
        }

        public string UpdateUser(UserDTO model, SessionDTO session)
        {
            string oldImagePath = userdao.UpdateUser(model, session);
            LogDAO.AddLog(General.ProcessType.UserUpdate, General.TableName.User, model.ID, session);
            return oldImagePath;
        }

        public string DeleteUser(int id, SessionDTO session)
        {
            string oldImagePath = userdao.DeleteUser(id, session);
            LogDAO.AddLog(General.ProcessType.UserDelete, General.TableName.User, id, session);
            return oldImagePath;
        }
    }
}