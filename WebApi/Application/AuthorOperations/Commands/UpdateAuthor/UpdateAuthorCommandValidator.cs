using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(3).When(x => x.Model.Name != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(2).When(x => x.Model.Surname != string.Empty);
            RuleFor(command => command.Model.DateOfBirth).LessThan(DateTime.Now.Date);
        }
        
    }
}
