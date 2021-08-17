#region

using FluentValidation;

#endregion

namespace Application.Commands.CreateClient
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(x => x.ClientName)
                .MinimumLength(2)
                .MaximumLength(64);
        }
    }
}