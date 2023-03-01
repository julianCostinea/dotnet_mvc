using System;
using System.Collections.Generic;
using System.Linq;
using DTO;

namespace DAL
{
    public class AddressDAO:PostContext
    {
        public int AddAddress(Address ads)
        {
            try
            {
                db.Addresses.Add(ads);
                db.SaveChanges();
                return ads.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<AddressDTO> GetAddresses()
        {
            List<Address> list = db.Addresses.Where(x=>x.isDeleted==false).ToList();
            List<AddressDTO> dtoList = new List<AddressDTO>();
            foreach (var item in list)
            {
                AddressDTO dto = new AddressDTO();
                dto.ID = item.ID;
                dto.AddressContent = item.Address1;
                dto.Email = item.Email;
                dto.Phone = item.Phone;
                dto.Phone2 = item.Phone2;
                dto.Fax = item.Fax;
                dto.LargeMapPath = item.MapPathLarge;
                dto.SmallMapPath = item.MapPathSmall;
                dtoList.Add(dto);
            }
            return dtoList;
        }

        public void UpdateAddress(AddressDTO model)
        {
            try
            {
                Address ads = db.Addresses.FirstOrDefault(x => x.ID == model.ID);
                ads.Address1 = model.AddressContent;
                ads.Email = model.Email;
                ads.Phone = model.Phone;
                ads.Phone2 = model.Phone2;
                ads.Fax = model.Fax;
                ads.MapPathLarge = model.LargeMapPath;
                ads.MapPathSmall = model.SmallMapPath;
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