using GraduationProject.Abstract;
using GraduationProject.Domains;

namespace GraduationProject.mocks
{
    public class MockOrders : IAllOrders
    {
        private readonly IOrderCategory _orderCategory = new MockCategory();

        public IEnumerable<openOrder> Orders { 
            get {
                return new List<openOrder> {
                    new openOrder {Name = "Тестовое название", 
                        ShortDesc = "Короткое описание", 
                        LongDesc = "Длинное описание", 
                        Img = " ", 
                        Price = 1, 
                        isOpen = true, 
                        CustomerId = " ", 
                        DeadLine = DateTime.Now.Date, 
                        CategoryOrder = _orderCategory.AllCategories.First()}
                };
            }
        }

        public openOrder GetObjectOrder(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
