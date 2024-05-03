using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibroApp.Models.Entities
{
    [Table("Libros")]
    public class Libro
    {
        [PrimaryKey]
        public int Id { get; set; }
        [NotNull]
        public string Titulo { get; set; } = null!;
        [NotNull]
        public string Autor { get; set; } = null!;
        [NotNull]
        public string Portada { get; set; } = null!;
        public bool ELiminado { get; set; }
    }
}
