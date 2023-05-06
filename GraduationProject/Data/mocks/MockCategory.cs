using GraduationProject.Abstract;
using GraduationProject.Models;

namespace GraduationProject.mocks
{
    public class MockCategory : IOrderCategory
    {
        public IEnumerable<CategoryOrder> AllCategories
        {
            get
            {
                return new List<CategoryOrder> 
                { 
                   new CategoryOrder { Name = "Программирование", Description = "Написание кода"},
                   new CategoryOrder { Name = "Дизайн", Description = "разботка дизайна под ваш продукт"}
                };
            }
        }
    }
}
