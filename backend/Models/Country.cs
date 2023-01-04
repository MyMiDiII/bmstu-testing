using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerING.Models {
    public class Country {

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int LevelOfInterest { get; set; }

        [Required]
        public int OverallPlayers { get; set; }
    }
}
