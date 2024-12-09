using FluentValidation;

namespace POD.API.Common.Core
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 25;

        public override string ToString()
        {
            return $"pageSize={PageSize}&pageNumber={PageNumber}";
        }
    }

    public class PaginationParametersValidator : AbstractValidator<Pagination>
    {
        public PaginationParametersValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .OverridePropertyName("page_size")
                .WithMessage("Page size must be between 1 and 100")
                .LessThanOrEqualTo(100)
                .OverridePropertyName("page_size")
                .WithMessage("Page size must be between 1 and 100");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(0)
                .WithName("page_after")
                .WithMessage("Page after must be greater then or equal to 0");
        }
    }
}