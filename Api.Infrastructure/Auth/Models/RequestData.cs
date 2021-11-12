using System;
using Api.Core.Auth;

namespace Api.Infrastructure.Auth.Models
{
    public class RequestData : IRequestData
    {
        public Guid UserId { get; }
        public string BusinessId { get; }

        public RequestData(Guid userId, string businessId)
        {
            UserId = userId;
            BusinessId = businessId;
        }
    }
}