using System.ComponentModel.DataAnnotations;

namespace ServerING.Models {
    public class Platform {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public ushort Popularity { get; set; }

        [Required]
        public int Cost { get; set; }
    }
}
