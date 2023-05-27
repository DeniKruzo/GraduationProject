using GraduationProject.Abstract;
using GraduationProject.Domains;
using GraduationProject.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Data.Repository
{
    public class OrderRepository : IAllOrders
    {
        private readonly GraduationDbContext appDbContext;
        public OrderRepository(GraduationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<openOrder> Orders => appDbContext.Orders.Include(c => c.CategoryOrder);

        public openOrder GetObjectOrder(long orderId) => appDbContext.Orders.FirstOrDefault(p => p.OrderId == orderId);
    }
}
