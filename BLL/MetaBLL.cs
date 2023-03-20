using System;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class MetaBLL
    {
        MetaDAO dao = new MetaDAO();

        public bool AddMeta(MetaDTO model, SessionDTO session)
        {
            Meta meta = new Meta();
            meta.Name = model.Name;
            meta.MetaContent = model.MetaContent;
            meta.AddDate = DateTime.Now;
            meta.LastUpdateDate = DateTime.Now;
            meta.LastUpdateUserID= session.UserID;
            int MetaID = dao.AddMeta(meta);
            LogDAO.AddLog(General.ProcessType.MetaAdd, General.TableName.Meta, MetaID, session);
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

        public bool UpdateMeta(MetaDTO model, SessionDTO session)
        {
            dao.UpdateMeta(model);
            LogDAO.AddLog(General.ProcessType.MetaUpdate, General.TableName.Meta, model.MetaID, session);
            return true;
        }

        public void DeleteMeta(int id, SessionDTO session)
        {
            dao.DeleteMeta(id);
            LogDAO.AddLog(General.ProcessType.MetaDelete, General.TableName.Meta, id, session);
        }
    }
}