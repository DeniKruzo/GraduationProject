using GraduationProject.Domains;

namespace GraduationProject.Abstract
{
    public interface IAllOrders
    {
        IQueryable<openOrder> Orders { get;}
        openOrder GetObjectOrder(long orderId);
    }
}
