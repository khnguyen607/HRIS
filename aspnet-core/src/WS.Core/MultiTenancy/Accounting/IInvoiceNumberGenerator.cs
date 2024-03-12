using System.Threading.Tasks;
using Abp.Dependency;

namespace WS.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}