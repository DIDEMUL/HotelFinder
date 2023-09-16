using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelFinder.Entities
{
    public class Hotel
    {
        //Öncelikle db de Hotels diye bir tablomuz olacak, hangi propertyleri içereceğini belirttik.
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//PK ve Identity olacak demiş olduk

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Required]
        public string City { get; set; }

        //Bundan sonraki aşamada DataAccess katmanımız var
    }
}
