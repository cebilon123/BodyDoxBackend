using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Application.Exceptions.User;
using Api.Application.Handlers.Queries;
using Api.Core.Auth;
using Api.Core.Domain;
using Api.Core.Exceptions.User;
using Api.Core.Repositories;
using Api.Core.ValueTypes;

namespace Api.Application.Handlers.QueryHandlers
{
    public class SignInQueryHandler : IQueryHandler<SignInQuery, SignInQueryResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IUserSessionsRepository _userSessionsRepository;

        public SignInQueryHandler(IUserRepository userRepository, IPasswordService passwordService,
            IJwtService jwtService, IUserSessionsRepository userSessionsRepository)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _userSessionsRepository = userSessionsRepository;
        }

        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        public async Task<SignInQueryResult> HandleAsync(SignInQuery query)
        {
            if (!((Email) query.Email).IsValid())
                throw new InvalidEmailException(query.Email);

            if (!((Password) query.Password).IsValid())
                throw new InvalidPasswordException();

            if (!await _userRepository.ExistsAsync(query.Email))
                throw new UserNotFoundException(query.Email);

            var user = await _userRepository.GetAsync(query.Email);

            // Large usage of memory because we are using Argon2id as hashing algorithm which use some memory.
            if (!_passwordService.AreTheSamePasswords(user.Password, query.Password))
                throw new PasswordsDontMatchException();

            var sessionId = Guid.NewGuid();
            var claims = new Dictionary<string, object>()
            {
                {"id", user.Id},
                {"businessId", "implement me after business"}, //TODO implement after business
                {"sessionId", sessionId}
            };

            var token = _jwtService.GenerateToken(claims);
            var refreshToken = _jwtService.GenerateRefreshToken(claims);

            await _userSessionsRepository.Insert(new UserSession(sessionId, user.Id,
                DateTime.Now.ToUniversalTime(), token, refreshToken));

            return new SignInQueryResult(token, refreshToken);
        }
    }
}