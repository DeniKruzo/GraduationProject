using GraduationProject.Domains;

namespace GraduationProject.Abstract
{
    public interface IAllOrders
    {
        IEnumerable<openOrder> Orders { get; }
        openOrder GetObjectOrder(int orderId);
    }
}
