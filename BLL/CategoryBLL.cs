using System.Collections.Generic;
using System.Web.Mvc;
using DAL;
using DTO;

namespace BLL
{
    public class CategoryBLL
    {
        CategoryDAO dao = new CategoryDAO();
        public bool AddCategory(CategoryDTO dto, SessionDTO session)
        {
            Category category = new Category();
            category.CategoryName = dto.CategoryName;
            category.AddDate = System.DateTime.Now;
            category.LastUpdateDate = System.DateTime.Now;
            category.LastUpdateUserID = session.UserID;
            int ID = dao.AddCategory(category);
            LogDAO.AddLog(General.ProcessType.CategoryAdd, General.TableName.Category, ID, session);
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

        public bool UpdateCategory(CategoryDTO dto, SessionDTO session)
        {
            dao.UpdateCategory(dto, session);
            LogDAO.AddLog(General.ProcessType.CategoryUpdate, General.TableName.Category, dto.ID, session);
            return true;
        }

        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            return CategoryDAO.GetCategoriesForDropdown();
        }
        PostBLL postbll = new PostBLL();
        public List<PostImageDTO> DeleteCategory(int id, SessionDTO session)
        {
            List<Post> postlist = dao.DeleteCategory(id, session);
            LogDAO.AddLog(General.ProcessType.CategoryDelete, General.TableName.Category, id, session);
            List<PostImageDTO> imagelist = new List<PostImageDTO>();
            foreach (var post in postlist)
            {
                List<PostImageDTO> imagelist2 = postbll.DeletePost(post.ID);
                LogDAO.AddLog(General.ProcessType.PostDelete, General.TableName.Post, post.ID, session);
                foreach (var image in imagelist2)
                {
                    imagelist.Add(image);
                }
            }
            return imagelist;
        }
    }
}