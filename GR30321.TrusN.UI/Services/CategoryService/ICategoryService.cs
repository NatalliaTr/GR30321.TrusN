using GR30321.Domain.Entities;
using GR30321.Domain.Models;

namespace GR30321.TrusN.UI.Services.CategoryService
{
    public interface ICategoryService
    {
        // Получение списка всех категорий
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
