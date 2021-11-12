using System;

namespace Api.Core.Auth
{
    public interface IRequestData
    {
        Guid UserId { get; }
        string BusinessId { get; } //TODO Refactor after business
    }
}