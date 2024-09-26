using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestWithASPNETErudio.Model.Base;

namespace RestWithASPNETErudio.Model.BookModel
{
    [Table("books")] // Define o nome da tabela no banco de dados
    public class BookModel : BaseEntity
    {
       
        [Column("author", TypeName = "longtext")]
        public string Author { get; set; }

        [Column("launch_date")]
        [Required] // Define que esse campo é obrigatório
        public DateTime LaunchDate { get; set; }

        [Column("price", TypeName = "decimal(65,2)")]
        [Required] // Define que esse campo é obrigatório
        public decimal Price { get; set; }

        [Column("title", TypeName = "longtext")]
        public string Title { get; set; }
    }
}