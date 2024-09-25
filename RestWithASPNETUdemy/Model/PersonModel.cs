using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETErudio.Model.PersonModel
{
    [Table("person")]
    public class PersonModel 
    {
        [Column("id")]
        public long Id { get; set;}

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