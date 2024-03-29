﻿using System;
using System.Collections.Generic;
using System.Linq;
using DTO;

namespace DAL
{
    public class MetaDAO
    {
        public int AddMeta(Meta meta)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.Metas.Add(meta);
                    db.SaveChanges();
                    return meta.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<MetaDTO> GetMetaData()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<MetaDTO> metaList = new List<MetaDTO>();
                List<Meta> list = db.Metas.Where(x => x.isDeleted == false).ToList();
                foreach (Meta item in list)
                {
                    MetaDTO dto = new MetaDTO();
                    dto.MetaID = item.ID;
                    dto.Name = item.Name;
                    dto.MetaContent = item.MetaContent;
                    metaList.Add(dto);
                }

                return metaList;
            }
        }

        public MetaDTO GetMetaWithID(int id)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Meta meta = db.Metas.FirstOrDefault(x => x.ID == id);
                MetaDTO dto = new MetaDTO();
                dto.MetaID = meta.ID;
                dto.Name = meta.Name;
                dto.MetaContent = meta.MetaContent;
                return dto;
            }
        }

        public void UpdateMeta(MetaDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Meta meta = db.Metas.FirstOrDefault(x => x.ID == model.MetaID);
                    meta.Name = model.Name;
                    meta.MetaContent = model.MetaContent;
                    meta.LastUpdateDate = DateTime.Now;
                    meta.LastUpdateUserID = UserStatic.UserID;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public void DeleteMeta(int id)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Meta meta = db.Metas.FirstOrDefault(x => x.ID == id);
                    meta.isDeleted = true;
                    meta.DeletedDate = DateTime.Now;
                    meta.LastUpdateDate = DateTime.Now;
                    meta.LastUpdateUserID = UserStatic.UserID;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}