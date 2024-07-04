using GR30321.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GR30321.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Uri проекта
            var uri = "https://localhost:7002/";
            
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Выполнение миграций
            await context.Database.MigrateAsync();

            // Заполнение данными
            if (!context.Categories.Any() && !context.Books.Any())
            {
                var categories = new Category[]
                {
                     
                new Category {Name="Художественная литература",
                                NormalizedName="ImaginativeLiterature"},
                new Category {Name="Детская литература",
                                NormalizedName="ChildLiterature"},
                new Category {Name="Бизнес-литература",
                                NormalizedName="BusinessLiterature"},
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
                var books = new List<Book>
                {
                   
                 new Book {Name ="Убийства по алфавиту", Avtor="Агата Кристи",
                            PublicationDate= 2019, Description ="Задача Пуаро – разгадать замыслы убийцы и не дать ему совершить задуманные 26 преступлений",
                            Price = 13.56, Image=uri+"Images/Agata_Kristi.jpg", Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("ImaginativeLiterature"))},

                new Book {Name ="Триумфальная арка", Avtor="Эрих Мария Ремарк",
                            PublicationDate= 2017, Description ="Пронзительная история любви всему наперекор, любви, приносящей боль, но и дарующей бесконечную радость",
                            Price = 20.19, Image=uri+"Images/Remark.jpg", Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("ImaginativeLiterature"))},

                new Book {Name ="Маркетинг от А до Я", Avtor="Филип Котлер",
                            PublicationDate= 2022, Description ="В книге в сжатой и понятной форме изложены 80 концепций эффективного маркетинга",
                            Price = 26.97, Image=uri + "Images/Marketing.jpg", Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("BusinessLiterature"))},

                new Book {Name ="Тимур и его команда", Avtor="Аркадий Гайдар",
                            PublicationDate= 2018, Description ="Повесть о пионерах довоенных лет",
                            Price = 5.65, Image=uri+"Images/Timur_i_ego_komanda.jpg", Category=categories.FirstOrDefault(c=>c.NormalizedName.Equals("ChildLiterature"))},

                };

                await context.AddRangeAsync(books);
                await context.SaveChangesAsync();
            }
        }

    }
}
