using FluentValidation;
using Todo.Minimalist.Api.DTOs;

namespace Todo.Minimalist.Api.Validators
{
    public class TodoItemUpdateDtoValidator : AbstractValidator<TodoItemUpdateDto>
    {
        public TodoItemUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título é obrigatório")
                .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres");

            RuleFor(x => x.IsDone)
                .NotNull().WithMessage("O campo IsDone é obrigatório");
        }
    }
}