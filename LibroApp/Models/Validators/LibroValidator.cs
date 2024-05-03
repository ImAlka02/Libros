using FluentValidation;
using LibroApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibroApp.Models.Validators
{
	public class LibroValidator : AbstractValidator<LibrosDTO>
	{
		public LibroValidator()
		{
			RuleFor(x => x.Titulo).NotEmpty().WithMessage("El titulo no debe estar vacio.");
			RuleFor(x => x.Autor).NotEmpty().WithMessage("El autor no debe estar vacio.");
			RuleFor(x => x.Portada).NotEmpty().WithMessage("La imagen de portaba no debe estar vacia.")
				.Must(ValidarURL).WithMessage("Escriba una dirección valida de una imagen JPG.");

		}

		public bool ValidarURL(string url)
		{
			if(url == null) return false;
			return url.StartsWith("https://") && url.EndsWith(".jpg");
		}
	}
}
