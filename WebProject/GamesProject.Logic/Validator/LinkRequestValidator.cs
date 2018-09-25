using FluentValidation;
using GamesProject.Logic.Common.Models;

namespace GamesProject.Logic.Validator
{
    public class LinkRequestValidator : AbstractValidator<LinkRequest>
    {
        public LinkRequestValidator()
        {
            RuleFor(link => link.Link).NotEmpty().NotNull();
        }
    }
}
