using System.ComponentModel.DataAnnotations;

namespace ServerING.Models {
    public class WebHosting {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int PricePerMonth { get; set; }

        [Required]
        public ushort SubMonths { get; set; }
    }
}
