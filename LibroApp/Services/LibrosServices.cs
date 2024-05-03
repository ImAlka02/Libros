using LibroApp.Models;
using LibroApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibroApp.Services
{
	public class LibrosServices
	{
		HttpClient cliente;
		Repos.LibroRepository repoLibro = new();
		public LibrosServices()
        {
			cliente = new()
			{
				BaseAddress = new Uri("http://libros.itesrc.net/")
			};

		}

		public async Task Agregar(LibrosDTO dto)
		{
			//var json = JsonSerializer.Serialize(dto);
			//var response = await cliente.PostAsync("api/libros", new StringContent(json, Encoding.UTF8, "application/json"));

			var response = await cliente.PostAsJsonAsync<LibrosDTO>("api/libros", dto);

			if (response.IsSuccessStatusCode)
			{
				await GetLibros();
			 
			}
			else
			{
				var errores = await response.Content.ReadAsStringAsync();
				throw new Exception(errores);
			}
		}
		public event Action? DatosActualizados;
		public async Task GetLibros()
		{
			try
			{
				var fecha = Preferences.Get("UltimaFechaActualizacion", DateTime.MinValue);

				var response = await cliente.GetFromJsonAsync<List<LibrosDTO>>($"api/libros/{fecha:yyyy-MM-dd}/{fecha:HH}/{fecha:mm}");
				bool aviso = false;
				if(response != null)
				{
					foreach (LibrosDTO libro in response)
					{
						var entidad = repoLibro.Get(libro.Id??0);

						if(entidad == null && libro.ELiminado == false)
						{
							entidad = new()
							{
								Id = libro.Id ?? 0,
								Autor = libro.Autor,
								ELiminado = libro.ELiminado,
								Portada = libro.Portada,
								Titulo = libro.Titulo
							};

							repoLibro.Insert(entidad);
							aviso = true;
						}
						else
						{
							if(entidad != null)
							{
								if (libro.ELiminado)
								{
									repoLibro.Delete(entidad);

								}
								else
								{
									if(libro.Titulo != entidad.Titulo | libro.Autor != entidad.Autor | libro.Portada != entidad.Portada)
									{
										repoLibro.Update(entidad);
										aviso = true;
									}
								}
							}
						}
					}

					if(aviso)
					{
						_ = MainThread.InvokeOnMainThreadAsync(() =>
						{
							DatosActualizados?.Invoke();
						});
					}
					Preferences.Set("UltimaFechaActualizacion", response.Max(x => x.Fecha));
				}
			}
			catch 
			{

			}
		}
    }
}
