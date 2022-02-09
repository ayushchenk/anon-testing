using AnonTesting.BLL.Commands.User;
using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using MediatR;

namespace AnonTesting.BLL.CommandHandlers.User
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result<Token>>
    {
        private readonly IMediator _mediator;

        public RegisterUserCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<Token>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var createResult = await _mediator.Send(new CreateUserCommand(command));

            if (createResult != null && createResult.Succeeded)
            {
                return await _mediator.Send(new LoginUserCommand(command));
            }

            string error = createResult?.Errors.FirstOrDefault()?.Description ?? string.Empty;
            return Result.Failure<Token>(error);
        }
    }
}
