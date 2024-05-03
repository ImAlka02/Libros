using ApiLibros.Models.DTOs;
using FluentValidation;

namespace ApiLibros.Models.Validators
{
	public class LibroValidator : AbstractValidator<LibroDTO>
	{
        public LibroValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El titulo no debe estar vacio.");
            RuleFor(x=> x.Autor).NotEmpty().WithMessage("El autor no debe estar vacio.");
            RuleFor(x=> x.Portada).NotEmpty().WithMessage("La imagen de portaba no debe estar vacia.")
                .Must(ValidarURL).WithMessage("Escriba una dirección valida de una imagen JPG.");

		}

        public bool ValidarURL(string url)
        {
            return url.StartsWith("https://") && url.EndsWith(".jpg");
        }
    }
}
