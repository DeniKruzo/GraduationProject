using GraduationProject.Abstract;
using GraduationProject.Models;

namespace GraduationProject.Data.Repository
{
    public class CategoryOfOrderRepository : IOrderCategory
    {
        private readonly GraduationDbContext appDbContext;
        public CategoryOfOrderRepository(GraduationDbContext appDbContext )
        {
                this.appDbContext = appDbContext;
        }
        public IEnumerable<CategoryOrder> AllCategories => appDbContext.CategoryOrder;
    }
}
