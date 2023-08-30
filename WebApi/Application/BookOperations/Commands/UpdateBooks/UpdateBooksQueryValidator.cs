using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdatedBook
{
    public class UpdateBooksQueryValidator : AbstractValidator<UpdatedBookCommand>
    {
        public UpdateBooksQueryValidator()
        {
            RuleFor(command => command.Model.Title).MinimumLength(4);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.AuthorId).GreaterThan(0);

        }
    }
}