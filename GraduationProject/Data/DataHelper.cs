using GraduationProject.Abstract;
using GraduationProject.Domains;
using GraduationProject.mocks;
using GraduationProject.Models;

namespace GraduationProject.Data
{
    public class DataHelper
    {
        /// <summary>
        /// Заполняем бд из локального репозитория (тестовы задания)
        /// </summary>
        /// <param name="app"></param>
        public static void Seed(GraduationDbContext context)
        {
            //if (!context.CategoryOrder.Any())
            //    context.CategoryOrder.AddRange(Categories.Select(c => c.Value));

            //if (!context.Orders.Any())
            //{
            //    context.AddRange(
            //        new openOrder
            //        {
            //            Name = "Тестовое название",
            //            ShortDesc = "Короткое описание",
            //            LongDesc = "Длинное описание",
            //            Img = "Картинка по умолчанию",
            //            Price = 1,
            //            isOpen = true,
            //            CustomerId = "ID заказчика",
            //            DeadLine = DateTime.Now.Date,
            //            CategoryOrder = category["Программирование"]
            //        },
            //        new openOrder
            //        {
            //            Name = "Тестовое название 2",
            //            ShortDesc = "Короткое описание",
            //            LongDesc = "Длинное описание",
            //            Img = "Картинка по умолчанию",
            //            Price = 2,
            //            isOpen = true,
            //            CustomerId = "ID заказчика",
            //            DeadLine = DateTime.Now.Date,
            //            CategoryOrder = category["Дизайн"]
            //        }
            //        );
            //}



            context.SaveChanges();
        }

        private static Dictionary<string, CategoryOrder> category;
        public static Dictionary<string,CategoryOrder> Categories
        {
            get
            {
                if(category == null)
                {
                    var list = new CategoryOrder[]
                    {
                         new CategoryOrder { Name = "Программирование", Description = "Написание кода"},
                         new CategoryOrder { Name = "Дизайн", Description = "разботка дизайна под ваш продукт"}
                    };

                    category = new Dictionary<string, CategoryOrder>();
                    foreach (CategoryOrder c in list)
                        category.Add(c.Name, c);
                }
                return category;
            }
        }
    }
}
