using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Core.Domain;
using Api.Core.Repositories;
using Api.Infrastructure.Repositories.Documents;

namespace Api.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoGenericRepository<UserDocument, Guid> _repository;

        public UserRepository(IMongoGenericRepository<UserDocument, Guid> repository)
        {
            _repository = repository;
        }


        public async Task<bool> ExistsAsync(string email)
        {
            return await _repository.ExistsAsync(u => u.Email == email);
        }

        public async Task Insert(User user)
        {
            await _repository.AddAsync(user.AsDocument());
        }

        public async Task<User> GetAsync(string email)
        {
            return (await _repository.GetAsync(u => u.Email == email)).AsEntity();
        }
    }
}