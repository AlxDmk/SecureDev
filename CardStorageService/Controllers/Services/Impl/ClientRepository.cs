using CardStorageService.Data;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CardStorageService.Controllers.Services.Impl
{
    public class ClientRepository : IClientRepository
    {
        private readonly CardStorageServiceDbContext _context;
        private readonly ILogger<CardRepository> _logger;

        public ClientRepository(CardStorageServiceDbContext context, ILogger<CardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public int Create(Client data)
        {
            _context.Add(data);
            _context.SaveChanges();
            return data.ClientId;
        }

        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Client> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Client GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Update(Client data)
        {
            throw new System.NotImplementedException();
        }
    }
}
