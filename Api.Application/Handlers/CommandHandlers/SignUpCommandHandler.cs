using System;
using System.Threading.Tasks;
using Api.Application.Handlers.Commands;
using Api.Core.Auth;
using Api.Core.Domain;
using Api.Core.Exceptions.User;
using Api.Core.Repositories;
using Api.Core.ValueTypes;

namespace Api.Application.Handlers.CommandHandlers
{
    public class SignUpCommandHandler : ICommandHandler<SignUpCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public SignUpCommandHandler(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task HandleAsync(SignUpCommand command)
        {
            if (!((Email) command.Email).IsValid())
                throw new InvalidEmailException(command.Email);

            if (!((Password) command.Password).IsValid())
                throw new InvalidPasswordException();

            if (await _userRepository.ExistsAsync(command.Email))
                throw new EmailOccupiedException(command.Email);

            var user = new User(
                Guid.NewGuid(),
                command.Email,
                _passwordService.HashPassword(command.Password),
                DateTime.Now.ToUniversalTime(),
                DateTime.Now.ToUniversalTime(),
                command.FirstName,
                command.LastName,
                command.PhoneNumber);

            await _userRepository.Insert(user);
        }
    }
}