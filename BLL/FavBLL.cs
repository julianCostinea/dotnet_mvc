﻿using DAL;
using DTO;

namespace BLL
{
    public class FavBLL
    {
        FavDAO dao = new FavDAO();
        public FavDTO GetFav()
        {
            return dao.GetFav();
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            FavDTO dto = new FavDTO();
            dto = dao.UpdateFav(model);
            LogDAO.AddLog(General.ProcessType.IconUpdate, General.TableName.Icon, dto.ID);
            return dto;
        }
    }
}