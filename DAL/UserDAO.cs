using System;
using System.Collections.Generic;
using System.Linq;
using DTO;

namespace DAL
{
    public class UserDAO : PostContext
    {
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            T_User user = db.T_User.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (user != null && user.ID != 0)
            {
                dto.ID = user.ID;
                dto.Username = user.Username;
                dto.Imagepath = user.ImagePath;
                dto.Name = user.NameSurname;
                dto.isAdmin = user.isAdmin;
            }

            return dto;
        }

        public int AddUser(T_User user)
        {
            try
            {
                db.T_User.Add(user);
                db.SaveChanges();
                return user.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<UserDTO> GetUsers()
        {
            List<T_User> list = db.T_User.Where(x => x.isDeleted == false).ToList();
            List<UserDTO> userlist = new List<UserDTO>();

            foreach (T_User user in list)
            {
                UserDTO dto = new UserDTO();
                dto.ID = user.ID;
                dto.Username = user.Username;
                dto.Imagepath = user.ImagePath;
                dto.Name = user.NameSurname;
                userlist.Add(dto);
            }

            return userlist;
        }

        public UserDTO GetUserWithID(int iD)
        {
            T_User user = db.T_User.FirstOrDefault(x => x.ID == iD);
            UserDTO dto = new UserDTO();
            dto.ID = user.ID;
            dto.Username = user.Username;
            dto.Password = user.Password;
            dto.Imagepath = user.ImagePath;
            dto.Name = user.NameSurname;
            dto.isAdmin = user.isAdmin;
            dto.Email = user.Email;
            return dto;
        }

        public string UpdateUser(UserDTO model)
        {
            try
            {
                T_User user = db.T_User.FirstOrDefault(x => x.ID == model.ID);
                string oldImagePath = user.ImagePath;
                user.NameSurname = model.Name;
                user.Username = model.Username;
                if (model.Imagepath != null)
                {
                    user.ImagePath = model.Imagepath;
                }
                user.Email = model.Email;
                user.Password = model.Password;
                user.isAdmin = model.isAdmin;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}