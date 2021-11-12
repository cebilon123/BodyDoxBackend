using System;

namespace Api.Core.Auth
{
    public class Token
    {
        /// <summary>
        /// Exp in seconds
        /// </summary>
        public long Exp { get; set; }

        public Guid Id { get; set; }

        public string BusinessId { get; set; }

        public Guid SessionId { get; set; }
    }
}