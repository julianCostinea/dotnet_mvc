using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web.Mvc;
using DTO;

namespace DAL
{
    public class CategoryDAO:PostContext
    {
        public int AddCategory(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return category.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<CategoryDTO> GetCategories()
        {
            List<Category> categories = db.Categories.Where(x=>x.isDeleted==false).ToList();
            List<CategoryDTO> dtolist = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                CategoryDTO dto = new CategoryDTO();
                dto.ID = category.ID;
                dto.CategoryName = category.CategoryName;
                dtolist.Add(dto);
            }

            return dtolist;
        }

        public CategoryDTO GetCategoryWithID(int id)
        {
            try
            {
                Category category = db.Categories.FirstOrDefault(x => x.ID == id);
                CategoryDTO dto = new CategoryDTO();
                dto.ID = category.ID;
                dto.CategoryName = category.CategoryName;
                return dto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateCategory(CategoryDTO dto)
        {
            try
            {
                Category category = db.Categories.FirstOrDefault(x => x.ID == dto.ID);
                category.CategoryName = dto.CategoryName;
                category.LastUpdateDate = DateTime.Now;
                category.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            IEnumerable<SelectListItem> categoryList = db.Categories.Where(x=>x.isDeleted==false).Select(x => new SelectListItem
            {
                Value = SqlFunctions.StringConvert((double)x.ID),
                Text = x.CategoryName
            }).ToList();
            return categoryList;
        }

        public List<Post> DeleteCategory(int id)
        {
            try
            {
                Category category = db.Categories.FirstOrDefault(x => x.ID == id);
                category.isDeleted = true;
                category.LastUpdateDate = DateTime.Now;
                category.LastUpdateUserID = UserStatic.UserID;
                category.isDeleted = true;
                db.SaveChanges();
                List<Post> postlist = db.Posts.Where(x => x.CategoryID == id).ToList();
                return postlist;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}