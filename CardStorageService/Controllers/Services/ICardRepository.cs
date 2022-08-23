using CardStorageService.Data;
using System.Collections.Generic;

namespace CardStorageService.Controllers.Services
{
    public interface ICardRepository : IRepository<Card, string>
    {
        IList<Card> GetByClientId(int id);
    }
}
