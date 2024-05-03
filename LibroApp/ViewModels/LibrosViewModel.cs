using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibroApp.Models.DTOs;
using LibroApp.Models.Entities;
using LibroApp.Models.Validators;
using LibroApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibroApp.ViewModels
{
    public partial class LibrosViewModel: ObservableObject
	{
		Repos.LibroRepository repoLibro = new();

        public LibrosViewModel()
        {
			ActualizarLibros();
			App.LibrosServices.DatosActualizados += LibrosServices_DatosActualizados;
		}

		private void LibrosServices_DatosActualizados()
		{
			ActualizarLibros();
		}

		public ObservableCollection<Libro> Libros { get; set; } = new();

		LibrosServices service = new();

		[ObservableProperty]
		private LibrosDTO? libroDTO;

		[ObservableProperty]
		private string error="";

        [RelayCommand]
		public void Nuevo()
		{
			LibroDTO = new();
			Shell.Current.GoToAsync("//Agregar");
		}

		[RelayCommand]
		public void Cancelar() 
		{
			LibroDTO = null;
			Error = "";
			Shell.Current.GoToAsync("//Lista");
		}

		LibroValidator validador = new();

		[RelayCommand]
		public async Task Agregar()
		{
			if(LibroDTO != null)
			{
				try
				{
					var resultado = validador.Validate(LibroDTO);
					if (resultado.IsValid)
					{

						await service.Agregar(LibroDTO);
						ActualizarLibros();
						Cancelar();
					}
					else
					{
						Error = string.Join("\n", resultado.Errors.Select(x => x.ErrorMessage));
					}
				}
				catch (Exception ex)
				{
					Error = ex.Message;
				}
			}
		}

		void ActualizarLibros()
		{
			Libros.Clear();
			foreach (var item in repoLibro.GetAll())
			{
				Libros.Add(item);
			}
		}
	}
}
