using System;
using System.Threading.Tasks;

namespace Api.Core.Auth
{
    public interface IRequestInfoProvider
    {
        Guid UserId { get; }
    }
}