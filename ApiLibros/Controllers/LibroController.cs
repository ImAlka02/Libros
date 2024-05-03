using ApiLibros.Models.DTOs;
using ApiLibros.Models.Entities;
using ApiLibros.Models.Validators;
using ApiLibros.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLibros.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LibroController : ControllerBase
	{
		private readonly LibroRepositorio repoLibro;

		public LibroController(LibroRepositorio repoLibro)
        {
			this.repoLibro = repoLibro;
		}

		[HttpPost]
		public IActionResult Post(LibroDTO libro)
		{
			//Validar
			LibroValidator validator = new();
			var resultado = validator.Validate(libro);

			if (resultado.IsValid)
			{
				Libros entity = new()
				{
					Id = 0,
					Eliminado = false,
					FechaActualizacion = DateTime.UtcNow,
					Autor = libro.Autor,
					Portada = libro.Portada,
					Titulo = libro.Titulo
				};
				repoLibro.Insert(entity);
				return Ok();
			}

			//Si es valido agrega
			return BadRequest(resultado.Errors.Select(x=>x.ErrorMessage));
		}

		[HttpGet("{fecha?}/{hora?}/{minutos?}")]
		public IActionResult Get(DateTime? fecha, int hora = 0, int minutos = 0)
		{
			if(fecha != null)
			{
				fecha = new(fecha.Value.Year, fecha.Value.Month, fecha.Value.Day, hora,minutos,0);
			
			}
			

			var libros = repoLibro.GetAll().Where(x => fecha == null || x.FechaActualizacion > fecha)
				.OrderBy(x => x.Titulo)
				.Select(x => new LibroDTO()
				{
					Id = x.Id,
					Titulo = x.Titulo,
					Autor = x.Autor,
					Portada = x.Portada,
					Eliminado = x.Eliminado,
					Fecha = x.FechaActualizacion
				});
			return Ok(libros);
		}

		[HttpPut]
		public IActionResult Put(LibroDTO libro) 
		{
			LibroValidator validator = new();
			var resultados = validator.Validate(libro);
			if(resultados.IsValid)
			{
				var entidadLibro = repoLibro.Get(libro.Id ?? 0);

				if(entidadLibro == null || entidadLibro.Eliminado == true)
				{
					return NotFound();
				}
				else
				{
					entidadLibro.Autor = libro.Autor;
					entidadLibro.Titulo = libro.Titulo;
					entidadLibro.Portada = libro.Portada;
					entidadLibro.FechaActualizacion = DateTime.UtcNow;

					repoLibro.Update(entidadLibro);
					return Ok(entidadLibro);	
				}

			}
			return BadRequest(resultados.Errors.Select(x=>x.ErrorMessage));
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var entidadLibro = repoLibro.Get(id);

			if(entidadLibro == null || entidadLibro.Eliminado == true)
			{
				return NotFound();
			}
			entidadLibro.Eliminado = true;
			entidadLibro.FechaActualizacion = DateTime.UtcNow;
			repoLibro.Update(entidadLibro);


			return Ok(entidadLibro);
		}
    }
}
