using FluentValidation;
using KUSYS.Domain.Entities;

namespace KUSYS.Application.Validators
{
    /// <summary>
    /// FluentValidation kullanılarak student sınıfı için validasyon tanımlaması yapıldı
    /// </summary>
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Öğrenci adı boş olamaz.")
                .MaximumLength(150);

            RuleFor(p => p.LastName)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Öğrenci soyadı boş olamaz.")
                .MaximumLength(150);

            RuleFor(p => p.StudentNo)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Öğrenci no boş olamaz")
                .MaximumLength(11)
                .MinimumLength(8)
                    .WithMessage("Öğrenci numarası 8 ile 11 karakter arasında olmalıdır.");
        }
    }
}
