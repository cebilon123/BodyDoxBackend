using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Core.Domain;


namespace Api.Core.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string email);
        Task Insert(User user);
        Task<User> GetAsync(string email);
    }
}