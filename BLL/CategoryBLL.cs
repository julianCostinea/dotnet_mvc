using System.Collections.Generic;
using System.Web.Mvc;
using DAL;
using DTO;

namespace BLL
{
    public class CategoryBLL
    {
        CategoryDAO dao = new CategoryDAO();
        public bool AddCategory(CategoryDTO dto)
        {
            Category category = new Category();
            category.CategoryName = dto.CategoryName;
            category.AddDate = System.DateTime.Now;
            category.LastUpdateDate = System.DateTime.Now;
            category.LastUpdateUserID = UserStatic.UserID;
            int ID = dao.AddCategory(category);
            LogDAO.AddLog(General.ProcessType.CategoryAdd, General.TableName.Category, ID);
            return true;
        }


        public List<CategoryDTO> GetCategories()
        {
            return dao.GetCategories();
        }

        public CategoryDTO GetCategoryWithID(int id)
        {
            return dao.GetCategoryWithID(id);
        }

        public bool UpdateCategory(CategoryDTO dto)
        {
            dao.UpdateCategory(dto);
            LogDAO.AddLog(General.ProcessType.CategoryUpdate, General.TableName.Category, dto.ID);
            return true;
        }

        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            return CategoryDAO.GetCategoriesForDropdown();
        }
    }
}