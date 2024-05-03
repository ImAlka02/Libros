using ApiLibros.Models.Entities;

namespace ApiLibros.Repositories
{
	public class LibroRepositorio
	{
		private readonly LibrosContext context;

		public LibroRepositorio(LibrosContext context)
        {
			this.context = context;
		}

		public IEnumerable<Libros> GetAll()
		{
			return context.Libros.OrderBy(x=> x.Titulo);
		}
		public Libros? Get(int id)
		{
			return context.Libros.Find(id);
		}
		
		public void Insert(Libros libro)
		{
			context.Libros.Add(libro);
			context.SaveChanges();
		}
		public void Update(Libros libro)
		{
			context.Libros.Update(libro);
			context.SaveChanges();
		}
		public void Delete(Libros libro)
		{
			context.Libros.Remove(libro);
			context.SaveChanges();
		}
    }
}
