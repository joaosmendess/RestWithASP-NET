using System.ComponentModel.DataAnnotations.Schema;
using RestWithASPNETErudio.Model.Base;

namespace RestWithASPNETErudio.Model.PersonModel
{
    [Table("person")]
    public class PersonModel : BaseEntity
    {
       

        [Column("first_name")]

        public string FirstName { get; set; }

        [Column("last_name")]

        public string LastName { get; set; }

        [Column("address")]

        public string Addres { get; set; }

        [Column("gender")]

        public string Genger { get; set; }

    }
}