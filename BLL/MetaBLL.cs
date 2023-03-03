using System;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class MetaBLL
    {
        MetaDAO dao = new MetaDAO();

        public bool AddMeta(MetaDTO model)
        {
            Meta meta = new Meta();
            meta.Name = model.Name;
            meta.MetaContent = model.MetaContent;
            meta.AddDate = DateTime.Now;
            meta.LastUpdateDate = DateTime.Now;
            meta.LastUpdateUserID= UserStatic.UserID;
            int MetaID = dao.AddMeta(meta);
            LogDAO.AddLog(General.ProcessType.MetaAdd, General.TableName.Meta, MetaID);
            return true;
        }

        public List<MetaDTO> GetMetaData()
        {
            List<MetaDTO> dtoList = new List<MetaDTO>();
            dtoList = dao.GetMetaData();
            return dtoList;
        }

        public MetaDTO GetMetaWithID(int id)
        {
            MetaDTO metadto = new MetaDTO();
            metadto = dao.GetMetaWithID(id);
            return metadto;
        }

        public bool UpdateMeta(MetaDTO model)
        {
            dao.UpdateMeta(model);
            LogDAO.AddLog(General.ProcessType.MetaUpdate, General.TableName.Meta, model.MetaID);
            return true;
        }

        public void DeleteMeta(int id)
        {
            dao.DeleteMeta(id);
            LogDAO.AddLog(General.ProcessType.MetaDelete, General.TableName.Meta, id);
        }
    }
}