using GR30321.Domain.Entities;
using GR30321.Domain.Models;
using GR30321.TrusN.UI.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace GR30321.TrusN.UI.Services.BookService
{
    public class MemoryBookService : IBookService
    {
        List<Book> _books;
        List<Category> _categories;
       private readonly IConfiguration _config;

        public MemoryBookService([FromServices] IConfiguration config, ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync()
           .Result
           .Data;
           _config = config;
            SetupData();
        }

        // Инициализация списков
        private void SetupData()
        {
            _books = new List<Book>
            {
                new Book {Id = 1, Name ="Убийства по алфавиту", Avtor="Агата Кристи",
                PublicationDate= 2019, Description ="Задача Пуаро – разгадать замыслы убийцы и не дать ему совершить задуманные 26 преступлений",
                Price = 13.56, Image="Images/Agata_Kristi.jpg", CategoryId=_categories.Find(c=>c.NormalizedName.Equals("ImaginativeLiterature")).Id, Category = _categories.Find(c => c.NormalizedName.Equals("ImaginativeLiterature")) },

                new Book {Id = 2, Name ="Триумфальная арка", Avtor="Эрих Мария Ремарк",
                PublicationDate= 2017, Description ="Пронзительная история любви всему наперекор, любви, приносящей боль, но и дарующей бесконечную радость",
                Price = 20.19, Image="Images/Remark.jpg", CategoryId=_categories.Find(c=>c.NormalizedName.Equals("ImaginativeLiterature")).Id, Category = _categories.Find(c=>c.NormalizedName.Equals("ImaginativeLiterature"))},

                new Book {Id = 3, Name ="Маркетинг от А до Я", Avtor="Филип Котлер",
                PublicationDate= 2022, Description ="В книге в сжатой и понятной форме изложены 80 концепций эффективного маркетинга",
                Price = 26.97, Image="Images/Marketing.jpg", CategoryId=_categories.Find(c=>c.NormalizedName.Equals("BusinessLiterature")).Id, Category = _categories.Find(c=>c.NormalizedName.Equals("BusinessLiterature")) },

                new Book {Id = 4, Name ="Тимур и его команда", Avtor="Аркадий Гайдар",
                PublicationDate= 2018, Description ="Повесть о пионерах довоенных лет",
                Price = 5.65, Image="Images/Timur_i_ego_komanda.jpg", CategoryId=_categories.Find(c=>c.NormalizedName.Equals("ChildLiterature")).Id, Category = _categories.Find(c => c.NormalizedName.Equals("ChildLiterature")) },

            };
        }
        public Task<ResponseData<Book>> CreateBookAsync(Book book, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Book>> DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Book>> GetBookByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ListModel<Book>>> GetBookListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            //var model = new ListModel<Book>() { Items = _books };
            //var result = new ResponseData<ListModel<Book>>()

            //{ Data = model };
            //return Task.FromResult(result);

            // Создать объект результата
            var result = new ResponseData<ListModel<Book>>();
            // Id категории для фильрации
            int? categoryId = null;
            // если требуется фильтрация, то найти Id категории с заданным categoryNormalizedName
            if (categoryNormalizedName != null)
                categoryId = _categories.Find(c =>c.NormalizedName.Equals(categoryNormalizedName))?.Id;
            // Выбрать объекты, отфильтрованные по Id категории, если этот Id имеется
            var data = _books
            .Where(d => categoryId == null ||
           d.CategoryId.Equals(categoryId))?
            .ToList();

            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();
            // получить общее количество страниц
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);
            // получить данные страницы
            var listData = new ListModel<Book>()
            {
                Items = data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };


            // поместить ранные в объект результата
            //result.Data = new ListModel<Book>() { Items = data };
           result.Data = listData;

            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }
            // Вернуть результат
            return Task.FromResult(result);

        }

        public Task<ResponseData<Book>> UpdateBookAsync(int id, Book book, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
