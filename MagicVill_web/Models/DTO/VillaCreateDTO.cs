using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVill_web.Models
{
    public class VillaCreateDTO
    {
        
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int Occupancy { get; set; }

        public int Sqft { get; set; }
        public string? ImageUrl { get; set; }
        public string? Amenity { get; set; }

        public string? Details { get; set; }

        [Required]
        public double Rate { get; set; }
    }
}
