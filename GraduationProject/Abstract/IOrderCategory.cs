using GraduationProject.Models;

namespace GraduationProject.Abstract
{
    public interface IOrderCategory
    {
        IEnumerable<CategoryOrder> AllCategories { get; }
    }
}
