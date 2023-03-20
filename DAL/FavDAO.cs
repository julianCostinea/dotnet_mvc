using System;
using System.Linq;
using DTO;

namespace DAL
{
    public class FavDAO : PostContext
    {
        public FavDTO GetFav()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                FavLogoTitle fav = db.FavLogoTitles.First();
                FavDTO dto = new FavDTO();
                dto.ID = fav.ID;
                dto.Title = fav.Title;
                dto.Logo = fav.Logo;
                dto.Fav = fav.Fav;
                return dto;
            }
        }

        public FavDTO UpdateFav(FavDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    FavLogoTitle fav = db.FavLogoTitles.First();
                    FavDTO dto = new FavDTO();
                    dto.ID = fav.ID;
                    dto.Fav = fav.Fav;
                    dto.Logo = fav.Logo;
                    fav.Title = model.Title;
                    if (model.Fav != null)
                    {
                        fav.Fav = model.Fav;
                    }

                    if (model.Logo != null)
                    {
                        fav.Logo = model.Logo;
                    }

                    db.SaveChanges();
                    return dto;
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