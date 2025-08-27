using EFakturCallback.Data.Entities;

namespace EFakturCallback.Data.Repositories.Interfaces
{
    public interface IEfakturListenerRepository
{
    Task EFakturListener(FakturDetailStream message);
}
}