using LibroApp.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibroApp.Repos
{
    public class LibroRepository
	{
		SQLiteConnection context;

		public LibroRepository()
		{
			string ruta = FileSystem.AppDataDirectory + "/libros.db3";
			context = new SQLiteConnection(ruta);

			context.CreateTable<Libro>();
		}

		public IEnumerable<Libro> GetAll() 
		{
			return context.Table<Libro>().OrderBy(x=>x.Titulo);
		}
		public Libro Get(int id)
		{
			return context.Find<Libro>(id);
		}
		public void InsertOrReplace(Libro l)
		{
			context.InsertOrReplace(l);	
		}
		public void Insert(Libro l)
		{
			context.Insert(l);
		}

		public void Update(Libro l) 
		{ 
			context.Update(l);
		}

		public void Delete(Libro l)
		{
			context.Delete(l);
		}
	}
}
