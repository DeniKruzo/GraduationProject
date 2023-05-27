using GraduationProject.Abstract;
using GraduationProject.Domains;

namespace GraduationProject.mocks
{
    public class MockOrders : IAllOrders
    {
        private readonly IOrderCategory _orderCategory = new MockCategory();

        public IQueryable<openOrder> Orders => new List<openOrder>
        {
            new openOrder {Name = "Тестовое название",
                ShortDesc = "Короткое описание",
                LongDesc = "Длинное описание",
                Img = "Картинка по умолчанию",
                Price = 1,
                isOpen = true,
                CustomerId = "ID заказчика",
                DeadLine = DateTime.Now.Date,
                CategoryOrder = _orderCategory.AllCategories.First()
            },
            new openOrder {Name = "Тестовое название 2",
                ShortDesc = "Короткое описание",
                LongDesc = "Длинное описание",
                Img = "Картинка по умолчанию",
                Price = 2,
                isOpen = true,
                CustomerId = "ID заказчика",
                DeadLine = DateTime.Now.Date,
                CategoryOrder = _orderCategory.AllCategories.Last()
            }
        }.AsQueryable();

        public openOrder GetObjectOrder(long orderId)
        {
            throw new NotImplementedException();
        }
    }
}
