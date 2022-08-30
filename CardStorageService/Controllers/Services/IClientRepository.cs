using CardStorageService.Data;

namespace CardStorageService.Controllers.Services
{
    public interface IClientRepository: IRepository<Client, int>
    {
    }
}
